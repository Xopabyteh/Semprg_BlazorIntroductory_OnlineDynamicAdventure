//ASP.NET Core hosted WASM app:

using Semprg_BlazorIntroductory.API.Endpoints;
using Semprg_BlazorIntroductory.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

//Add blazor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

await app.InitializeInfrastructure(rootNodeStoryContent: "Once upon a time...");

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

//Map API Endpoints:
app.AddGameEndpoints();

//Map razor
app.MapRazorPages();
app.MapFallbackToFile("index.html");

app.Run();

