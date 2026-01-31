using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using XrayOldworldLauncher;
using TauriApi;
using MudBlazor.Services;
using XrayOldworldLauncher.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTauriApi();
builder.Services.AddMudServices();
builder.Services.AddScoped<OptionsStateService>();
builder.Services.AddScoped<LocalizationService>();

await builder.Build().RunAsync();
