using Microsoft.AspNetCore.HttpOverrides;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using AssemblyAI.Realtime;
using Twilio.AspNet.Core;
using Twilio.TwiML;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ForwardedHeadersOptions>(
    options => options.ForwardedHeaders = ForwardedHeaders.All
);
builder.Services.AddTransient<RealtimeTranscriber>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var realtimeTranscriber = new RealtimeTranscriber
    {
        ApiKey = config["AssemblyAI:ApiKey"]!,
        SampleRate = 8000,
        Encoding = "pcm_mulaw"
    };
    return realtimeTranscriber;
});

var app = builder.Build();
app.UseForwardedHeaders();
app.UseWebSockets();

app.MapGet("/", () => "Hello World!");

app.MapPost("/voice", (HttpRequest request) =>
{
    var response = new VoiceResponse();
    response.Say("Speak to see your audio transcribed in the console.");
    var connect = new Twilio.TwiML.Voice.Connect();
    connect.Stream(url: $"wss://{request.Host}/stream");
    response.Append(connect);
    return Results.Extensions.TwiML(response);
});

app.MapGet("/stream", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await TranscribeStream(webSocket, context.RequestServices);
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
    }
});

async Task TranscribeStream(
    WebSocket webSocket,
    IServiceProvider serviceProvider
)
{
    var appLifetime = serviceProvider.GetRequiredService<IHostApplicationLifetime>();
    var realtimeTranscriber = serviceProvider.GetRequiredService<RealtimeTranscriber>();

    var transcriptWords = new SortedDictionary<int, string>();

    string BuildTranscript()
    {
        var stringBuilder = new StringBuilder();
        foreach (var word in transcriptWords.Values)
        {
            stringBuilder.Append($"{word} ");
        }

        return stringBuilder.ToString();
    }

    realtimeTranscriber.SessionBegins += (sender, evt) =>
        app.Logger.LogInformation(
            "RealtimeTranscriber session begins with ID {SessionId} until {ExpiresAt}",
            evt.Result.SessionId,
            evt.Result.ExpiresAt
        );
    realtimeTranscriber.ErrorReceived += (sender, evt) =>
        app.Logger.LogError("RealtimeTranscriber error: {error}", evt.Error);
    realtimeTranscriber.Closed += (sender, evt) =>
        app.Logger.LogWarning("RealtimeTranscriber closed with status {Code}, reason: {Reason}", evt.Code, evt.Reason);

    realtimeTranscriber.PartialTranscriptReceived += (sender, evt) =>
    {
        if (string.IsNullOrEmpty(evt.Result.Text)) return;
        foreach (var word in evt.Result.Words)
        {
            transcriptWords[word.Start] = word.Text;
        }

        var transcript = BuildTranscript();
        Console.Clear();
        Console.WriteLine(transcript);
    };

    realtimeTranscriber.FinalTranscriptReceived += (sender, evt) =>
    {
        foreach (var word in evt.Result.Words)
        {
            transcriptWords[word.Start] = word.Text;
        }

        var transcript = BuildTranscript();
        Console.Clear();
        Console.WriteLine(transcript);
    };

    await realtimeTranscriber.ConnectAsync().ConfigureAwait(false);

    var buffer = new byte[1024 * 4];
    var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

    while (!receiveResult.CloseStatus.HasValue &&
           !appLifetime.ApplicationStopping.IsCancellationRequested)
    {
        using var jsonDocument = JsonSerializer.Deserialize<JsonDocument>(buffer.AsSpan(0, receiveResult.Count));
        var eventMessage = jsonDocument.RootElement.GetProperty("event").GetString();

        switch (eventMessage)
        {
            case "connected":
                app.Logger.LogInformation("Twilio media stream connected");
                break;
            case "start":
                app.Logger.LogInformation("Twilio media stream started");
                break;
            case "media":
                var payload = jsonDocument.RootElement.GetProperty("media").GetProperty("payload").GetString();
                byte[] audio = Convert.FromBase64String(payload);
                realtimeTranscriber.SendAudio(audio);
                break;
            case "stop":
                app.Logger.LogInformation("Twilio media stream stopped");
                break;
        }

        receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    }

    if (receiveResult.CloseStatus.HasValue)
    {
        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
    }
    else if (appLifetime.ApplicationStopping.IsCancellationRequested)
    {
        await webSocket.CloseAsync(
            WebSocketCloseStatus.EndpointUnavailable,
            "Server shutting down",
            CancellationToken.None);
    }

    await realtimeTranscriber.CloseAsync();
}

app.Run();