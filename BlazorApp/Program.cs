using BlazorApp;
using KooliProjekt.PublicApi.Api;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7136/api/") });
builder.Services.AddScoped<IApiClient, ApiClient>();

await builder.Build().RunAsync();
