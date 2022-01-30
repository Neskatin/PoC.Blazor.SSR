using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Identity.Web;
using Microsoft.JSInterop;
using PoC.Blazor.SSR.Client;
using PoC.Blazor.SSR.Shared.Rendering;
using JSRuntime = PoC.Blazor.SSR.Server.Mocks.JSRuntime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Client dependencies
builder.Services.AddSingleton<RenderLocation, RenderedOnServer>();

builder.Services.AddHttpClient("PoC.Blazor.SSR.ServerAPI",
        (sp, client) => client.BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddClient(builder.Configuration);
builder.Services.AddTransient<IJSRuntime, JSRuntime>();

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();