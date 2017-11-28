using HtmlRapier.TagHelpers;
using HtmlRapier.TagHelpers.ClientConfig;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class AssetBundleServiceExtensions
    {
        /// <summary>
        /// Adds configuration for a tag helper that can output bundles or their content files depending on configuration.
        /// </summary>
        public static IServiceCollection AddAssetBundle(this IServiceCollection services, Action<AssetBundleOptions> setupOptions)
        {
            var options = new AssetBundleOptions();
            setupOptions(options);
            services.TryAddSingleton<AssetBundleOptions>(options);

            return services;
        }
    }
}
