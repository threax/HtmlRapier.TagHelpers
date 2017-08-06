using HtmlRapier.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        public static IServiceCollection AddClientConfig(this IServiceCollection services, IClientConfig clientConfig)
        {
            services.TryAddSingleton<IClientConfig>(clientConfig);

            return services;
        }
    }
}
