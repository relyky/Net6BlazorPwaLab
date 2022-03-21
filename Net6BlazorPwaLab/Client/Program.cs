using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Net6BlazorPwaLab.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
  BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
  DefaultRequestVersion = new Version(2, 0)
}); ;

await builder.Build().RunAsync();
