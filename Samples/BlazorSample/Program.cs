using AssemblyAI;
using BlazorSample;
using BlazorSample.Components;

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
builder.Services.AddScoped<AppState>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
     app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.MapPost("/api/realtime/token", async (AssemblyAI.AssemblyAI assemblyAIClient) =>
{
    var tokenResponse = await assemblyAIClient.Realtime.CreateTemporaryToken(new CreateRealtimeTemporaryTokenParameters
    {
        ExpiresIn = 360
    });
    return tokenResponse;
});

app.Run();