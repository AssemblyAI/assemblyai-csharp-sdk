using AssemblyAI;
using BlazorSample.Server;
using BlazorSample.Server.Components;
using BlazorSample.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();

builder.Services.AddTransient<AssemblyAIClient>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    return new AssemblyAIClient(config["AssemblyAI:ApiKey"]);
});
builder.Services.AddScoped<IRealtimeTranscriberFactory, RealtimeTranscriberFactory>();
builder.Services.AddScoped<IFileTranscriber, FileTranscriber>();
builder.Services.AddScoped<IAskLemur, AskLemur>();

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
    .AddAdditionalAssemblies(typeof(BlazorSample.Shared.Components.Pages.TranscribeFile).Assembly);

app.MapApiEndpoints();

app.Run();