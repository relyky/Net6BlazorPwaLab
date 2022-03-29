using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Net6BlazorPwaLab.Client;
using Net6BlazorPwaLab.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region ## Add services to the container. -------------------------------------

builder.Services.AddScoped<BlazorComponentBus.ComponentBus>();
builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient
{
  BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
  DefaultRequestVersion = new Version(2, 0)
});

builder.Services.AddScoped<WebApiService>();
builder.Services.AddScoped<StateContainer>();
builder.Services.AddScoped<JSFooService>();

#endregion --------------------------------------------------------------------

await builder.Build().RunAsync();
