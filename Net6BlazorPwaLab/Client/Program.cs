using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Net6BlazorPwaLab.Client;
using Net6BlazorPwaLab.Client.Services;
using MediatR;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region ## Add services to the container. -------------------------------------

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddScoped<BlazorComponentBus.ComponentBus>();

builder.Services.AddScoped(sp => new HttpClient
{
  BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
  DefaultRequestVersion = new Version(2, 0)
});

builder.Services.AddScoped<WebApiService>();

#endregion --------------------------------------------------------------------

await builder.Build().RunAsync();
