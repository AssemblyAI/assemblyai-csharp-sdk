using BlazorSample.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorSample.Wasm;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddScoped<IRealtimeTranscriberFactory, RealtimeTranscriberFactory>();
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        await builder.Build().RunAsync();
    }
}