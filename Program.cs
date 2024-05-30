using BlazorApp;
using BlazorApp.Services;
using BlazorApp.Services.Karp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped<IStudentProvider,  StudentProvider>();
builder.Services.AddScoped<IGraphProvider, GraphProvider>();
builder.Services.AddScoped<IKarpProvider, KarpProvider>();

await builder.Build().RunAsync();
