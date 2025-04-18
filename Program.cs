using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IDK;

// TODO: fix the prerendering when publish using BlazorWasmPreRendering.Build not working on github pages

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();

static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
    services.AddLocalStorageServices();
    services.AddScoped<IAIService, AIService>();
}
