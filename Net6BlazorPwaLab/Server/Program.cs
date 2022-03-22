using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

#region ## Add services to the container. -------------------------------------

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

#endregion --------------------------------------------------------------------

var app = builder.Build();

#region ## Configure the HTTP request pipeline. -------------------------------
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

//## Endpoints
app.MapRazorPages();
app.MapControllers(); // for enable WebApi
app.MapFallbackToFile("index.html");

#endregion --------------------------------------------------------------------

app.Run();
