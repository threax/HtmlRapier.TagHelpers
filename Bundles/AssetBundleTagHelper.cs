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
        private FileVersionProvider fileVersionProvider;
        private IMemoryCache cache;

        public AssetBundleTagHelper(IUrlHelperFactory urlHelperFactory, IHostingEnvironment hostingEnvironment, AssetBundleOptions options, IMemoryCache cache)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.urlHelperFactory = urlHelperFactory;
            this.options = options;
            this.cache = cache;
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
            EnsureFileVersionProvider();
            var format = cssFormat;

            if (Path.GetExtension(Src).Equals(".js", StringComparison.InvariantCultureIgnoreCase))
            {
                format = jsFormat;
            }

            foreach (var file in inputFiles)
            {
                var vFile = fileVersionProvider.AddFileVersionToPath(file);
                output.Content.AppendHtmlLine(String.Format(format, vFile));
            }
        }

        private void EnsureFileVersionProvider()
        {
            if (fileVersionProvider == null)
            {
                fileVersionProvider = new FileVersionProvider(
                    hostingEnvironment.WebRootFileProvider,
                    cache,
                    ViewContext.HttpContext.Request.PathBase);
            }
        }

        private IEnumerable<String> Files()
        {
            if (options.UseBundles)
            {
                yield return urlHelper.Content(Src);
            }
            else
            {
                var baseFolder = hostingEnvironment.ContentRootPath;
                var configFile = Path.Combine(baseFolder, "bundleconfig.json");
                var bundle = GetBundle(configFile, Src);
                if (bundle == null)
                {
                    yield break;
                }

                foreach (var file in bundle.InputFiles)
                {
                    yield return file.Substring(options.UnbundledFolder.Length);
                }
            }
        }

        //Some of this taken from
        //https://gist.github.com/mohamedmansour/cd50123f8575daba7a7f12847b12da5d

        private Bundle GetBundle(string configFile, string bundlePath)
        {
            var file = new FileInfo(configFile);
            if (!file.Exists)
                return null;

            bundlePath = urlHelper.Content(bundlePath);

            var bundles = JsonConvert.DeserializeObject<IEnumerable<Bundle>>(File.ReadAllText(configFile));
            return (from b in bundles
                    where b.OutputFileName.EndsWith(bundlePath, StringComparison.InvariantCultureIgnoreCase)
                    select b).FirstOrDefault();
        }

        class Bundle
        {
            [JsonProperty("outputFileName")]
            public string OutputFileName { get; set; }

            [JsonProperty("inputFiles")]
            public List<string> InputFiles { get; set; } = new List<string>();
        }
    }
}
