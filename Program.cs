using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IDK;
using Microsoft.JSInterop;

// TODO: fix the prerendering when publish using BlazorWasmPreRendering.Build not working on github pages

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services, builder.HostEnvironment, builder.Configuration);

await builder.Build().RunAsync();

static void ConfigureServices(IServiceCollection services, IWebAssemblyHostEnvironment webHostEnv, IConfiguration configuration)
{
    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(webHostEnv.BaseAddress) });

    // If we are currently prerendering
    if(webHostEnv.IsEnvironment("Prerendering"))
    {
        // Add no op local storage service
        services.AddScoped<ILocalStorageService, NoOpLocalStorageService>();
    }
    // Otherwise, the app is running normally
    else
    {
        // Add the real local storage service
        services.AddLocalStorageServices();
    }

    services.AddScoped<IAIService, AIService>();
}
