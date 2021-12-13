using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PoC.Blazor.SSR.Client;
using PoC.Blazor.SSR.Shared.Rendering;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<RenderLocation, RenderedOnClient>();

builder.Services.AddClient(builder.Configuration, builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();