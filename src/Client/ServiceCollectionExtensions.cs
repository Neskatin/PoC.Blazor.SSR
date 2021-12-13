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

        services.AddMsalAuthentication(options =>
        {
            config.Bind("AzureAd", options.ProviderOptions.Authentication);
            options.ProviderOptions.DefaultAccessTokenScopes.Add("api://api.id.uri/access_as_user");
        });

        return services;
    }
}