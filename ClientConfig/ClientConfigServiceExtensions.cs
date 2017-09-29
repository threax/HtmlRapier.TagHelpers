using HtmlRapier.TagHelpers;
using HtmlRapier.TagHelpers.ClientConfig;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class TagHelperSetupExtensions
    {
        /// <summary>
        /// Add the page configuration as a singleton to the services.
        /// </summary>
        /// <param name="services">The service collection to add to.</param>
        /// <param name="clientConfig">The page config to set.</param>
        /// <returns></returns>
        public static IServiceCollection AddClientConfig(this IServiceCollection services, IClientConfig clientConfig, Action<ClientConfigTagHelperOptions> setupOptions)
        {
            var options = new ClientConfigTagHelperOptions();
            setupOptions(options);
            services.TryAddSingleton<ClientConfigTagHelperOptions>(options);
            services.TryAddSingleton<IClientConfig>(clientConfig);

            return services;
        }
    }
}
