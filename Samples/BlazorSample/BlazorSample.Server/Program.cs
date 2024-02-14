using AssemblyAI;
using BlazorSample.Server;
using BlazorSample.Server.Components;
using BlazorSample.Server.Models;
using BlazorSample.Shared;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();

builder.Services.AddTransient<AssemblyAI.AssemblyAI>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    return new AssemblyAI.AssemblyAI(config["AssemblyAI:ApiKey"]);
});
builder.Services.AddScoped<IRealtimeTranscriberFactory, RealtimeTranscriberFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorSample.Wasm.Program).Assembly)
    .AddAdditionalAssemblies(typeof(BlazorSample.Shared.Pages.TranscribeFile).Assembly);

var api = app.MapGroup("/api");

api.MapPost("/transcribe-file", [RequestSizeLimit(2_306_867_200)] async (
    [FromForm] TranscribeFileModel model,
    AssemblyAI.AssemblyAI assemblyAIClient
) =>
{
    await using var fileStream = model.File.OpenReadStream();
    var fileUpload = await assemblyAIClient.Files.Upload(await ReadToEndAsync(fileStream));
    var transcript = await assemblyAIClient.Transcript.Create(new CreateTranscriptParameters
    {
        AudioUrl = fileUpload.UploadUrl,
        LanguageCode = new TranscriptLanguageCode(model.LanguageCode)
    });
    return transcript;
});

api.MapPost("/realtime/token", async (AssemblyAI.AssemblyAI assemblyAIClient) =>
{
    var tokenResponse = await assemblyAIClient.Realtime.CreateTemporaryToken(new CreateRealtimeTemporaryTokenParameters
    {
        ExpiresIn = 360
    });
    return tokenResponse;
});

app.Run();

// TODO: Replace when stream is supported by SDK
async Task<byte[]> ReadToEndAsync(Stream stream)
{
    long originalPosition = 0;

    if (stream.CanSeek)
    {
        originalPosition = stream.Position;
        stream.Position = 0;
    }

    var totalBytesRead = 0;
    try
    {
        var readBuffer = new byte[4096];

        int bytesRead;

        while ((bytesRead = await stream.ReadAsync(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)
                   .ConfigureAwait(false)) > 0)
        {
            totalBytesRead += bytesRead;

            if (totalBytesRead != readBuffer.Length) continue;
            var nextByte = stream.ReadByte();
            if (nextByte == -1) continue;
            var temp = new byte[readBuffer.Length * 2];
            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
            readBuffer = temp;
            totalBytesRead++;
        }

        var buffer = readBuffer;
        if (readBuffer.Length == totalBytesRead) return buffer;
        buffer = new byte[totalBytesRead];
        Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);

        return buffer;
    }
    finally
    {
        if (stream.CanSeek)
        {
            stream.Position = originalPosition;
        }
    }
}