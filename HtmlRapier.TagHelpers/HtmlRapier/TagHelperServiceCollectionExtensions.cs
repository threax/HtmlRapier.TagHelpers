using HtmlRapier.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection.Extensions
{
    public static class TagHelperServiceCollectionExtensions
    {
        /// <summary>
        /// Add configuration for the SpcTagHelpers. This is optional and only needs to be called if you want
        /// to configure the options.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="optionsCallback">A callback to configure the options.</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureHtmlRapierTagHelpers(this IServiceCollection services, Action<HtmlRapierTagHelperOptions> optionsCallback = null)
        {
            var options = new HtmlRapierTagHelperOptions();
            optionsCallback?.Invoke(options);
            services.AddSingleton<HtmlRapierTagHelperOptions>(options);

            switch (options.FrontEndLibrary)
            {
                case FrontEndLibrary.Bootstrap3:
                    services.TryAddTransient<IPageNumbers, PageNumbersBootstrap3>();
                    services.TryAddTransient<IModal, ModalBootstrap3>();
                    services.TryAddTransient<IRelogin, ReloginBootstrap3>();
                    break;
                case FrontEndLibrary.Bootstrap4:
                    services.TryAddTransient<IPageNumbers, PageNumbersBootstrap4>();
                    services.TryAddTransient<IModal, ModalBootstrap4>();
                    services.TryAddTransient<IRelogin, ReloginBootstrap4>();
                    break;
            }

            return services;
        }
    }
}
