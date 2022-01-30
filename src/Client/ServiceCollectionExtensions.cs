using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace PoC.Blazor.SSR.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration config, string? baseAddress = null)
    {
        if (baseAddress != null)
        {
            services.AddHttpClient("PoC.Blazor.SSR.ServerAPI", client => client.BaseAddress = new Uri(baseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
        }

        // Supply HttpClient instances that include access tokens when making requests to the server project
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PoC.Blazor.SSR.ServerAPI"));

        services.AddOidcAuthentication(options =>
        {
            config.Bind("Auth0", options.ProviderOptions);
            options.ProviderOptions.ResponseType = "code";
        });

        return services;
    }
}