using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Net6BlazorPwaLab.Client;
using Net6BlazorPwaLab.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
  BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
  DefaultRequestVersion = new Version(2, 0)
});

builder.Services.AddScoped<WebApiService>();

await builder.Build().RunAsync();
