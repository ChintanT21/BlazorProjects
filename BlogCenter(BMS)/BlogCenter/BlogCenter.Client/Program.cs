using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using BlogCenter.Shared.ClientServices;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddAuthorizationCore();
builder.Services.AddMudServices();
await builder.Build().RunAsync();