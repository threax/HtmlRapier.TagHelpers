using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers.Internal;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HtmlRapier.TagHelpers
{
    public class AssetBundleTagHelper : TagHelper
    {
        private const String jsFormat = "<script src='{0}' type='text/javascript'></script>";
        private const String cssFormat = "<link rel='stylesheet' href='{0}' />";

        private IHostingEnvironment hostingEnvironment;
        private IUrlHelperFactory urlHelperFactory;
        private ViewContext viewContext;
        private IUrlHelper urlHelper;
        private AssetBundleOptions options;
        private IFileVersionProvider fileVersionProvider;
        private IMemoryCache cache;

        public AssetBundleTagHelper(IUrlHelperFactory urlHelperFactory, IHostingEnvironment hostingEnvironment, AssetBundleOptions options, IMemoryCache cache, IFileVersionProvider fileVersionProvider)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.urlHelperFactory = urlHelperFactory;
            this.options = options;
            this.cache = cache;
            this.fileVersionProvider = fileVersionProvider;
        }

        public String Src { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext
        {
            get
            {
                return this.viewContext;
            }
            set
            {
                this.viewContext = value;
                this.urlHelper = urlHelperFactory.GetUrlHelper(viewContext);
            }
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var inputFiles = Files();
            output.TagName = null;
            var format = cssFormat;

            if (Path.GetExtension(Src).Equals(".js", StringComparison.InvariantCultureIgnoreCase))
            {
                format = jsFormat;
            }

            foreach (var file in inputFiles)
            {
                var vFile = file;
                var first = vFile[0];
                if (first != '~')
                {
                    if (first != '/' && first != '\\')
                    {
                        vFile = "~/" + vFile;
                    }
                    else
                    {
                        vFile = "~" + vFile;
                    }
                }
                vFile = urlHelper.Content(vFile);
                vFile = fileVersionProvider.AddFileVersionToPath("", vFile);
                output.Content.AppendHtmlLine(String.Format(format, vFile));
            }
        }

        private IEnumerable<String> Files()
        {
            if (options.UseBundles)
            {
                yield return Src;
            }
            else
            {
                var baseFolder = hostingEnvironment.ContentRootPath;

                //Look in artifacts.json
                bool keepLooking = false;
                var configFile = Path.Combine(baseFolder, options.ArtifactsJsonFileName);
                if (File.Exists(configFile))
                {
                    var bundle = GetArtifactsBundle(configFile, Src);
                    if (bundle == null)
                    {
                        keepLooking = true;
                    }
                    else
                    {
                        foreach (var file in bundle.Input)
                        {
                            yield return file.Substring(options.UnbundledFolder.Length);
                        }
                    }
                }

                if (keepLooking)
                {
                    //Look in bundler minifier core
                    keepLooking = false;
                    configFile = Path.Combine(baseFolder, options.BundlerMinifierCoreFileName);
                    if (File.Exists(configFile))
                    {
                        var bundle = GetBundlerMinifierCore(configFile, Src);
                        if (bundle == null)
                        {
                            keepLooking = true;
                        }
                        else
                        {
                            foreach (var file in bundle.InputFiles)
                            {
                                yield return file.Substring(options.UnbundledFolder.Length);
                            }
                        }
                    }
                }

                if (keepLooking)
                {
                    //Didn't find anything, throw exception
                    throw new FileNotFoundException($"Cannot find bundle {Src} in {configFile}");
                }
            }
        }

        /// <summary>
        /// Get the bundles from BundlerMinifierCore.
        /// </summary>
        /// <param name="configFile"></param>
        /// <param name="bundlePath"></param>
        /// <returns></returns>
        //Some of this taken from
        //https://gist.github.com/mohamedmansour/cd50123f8575daba7a7f12847b12da5d

        private BundlerMinifierCoreBundle GetBundlerMinifierCore(string configFile, string bundlePath)
        {
            var file = new FileInfo(configFile);
            if (!file.Exists)
            {
                throw new FileNotFoundException($"Cannot find bundle bundle config file {configFile}");
            }

            //Strip leading ~
            if(bundlePath[0] == '~')
            {
                bundlePath = bundlePath.Substring(1);
            }

            var bundles = JsonConvert.DeserializeObject<IEnumerable<BundlerMinifierCoreBundle>>(File.ReadAllText(configFile));
            return (from b in bundles
                    where b.OutputFileName.EndsWith(bundlePath, StringComparison.InvariantCultureIgnoreCase)
                    select b).FirstOrDefault();
        }

        /// <summary>
        /// Get the bundles from artifacts.json
        /// </summary>
        /// <param name="configFile"></param>
        /// <param name="bundlePath"></param>
        /// <returns></returns>
        private ArtifactsJsonBundleOptions GetArtifactsBundle(string configFile, string bundlePath)
        {
            var file = new FileInfo(configFile);
            if (!file.Exists)
            {
                throw new FileNotFoundException($"Cannot find bundle bundle config file {configFile}");
            }

            //Strip leading ~/
            if (bundlePath[0] == '~' && (bundlePath[1] == '/' || bundlePath[1] == '\\'))
            {
                bundlePath = bundlePath.Substring(2);
            }

            var bundles = JsonConvert.DeserializeObject<IEnumerable<ArtifactsJsonBundle>>(File.ReadAllText(configFile));

            foreach(var bundle in bundles.Where(i => i.bundle != null))
            {
                foreach(var options in bundle.bundle)
                {
                    if (options.Out?.EndsWith(bundlePath) == true)
                    {
                        return options;
                    }
                }
            }

            return null;
        }
    }
}
