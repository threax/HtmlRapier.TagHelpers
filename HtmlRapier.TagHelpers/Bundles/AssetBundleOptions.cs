using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlRapier.TagHelpers
{
    public class AssetBundleOptions
    {
        /// <summary>
        /// Set this to true to use asset bundles. Defaults to false, which will unpack the bundles
        /// and return the original files. Defaults to false.
        /// </summary>
        public bool UseBundles { get; set; } = false;

        /// <summary>
        /// The folder that contains the unbundled assets. Should be accessible by the web server.
        /// Defaults to "wwwroot/".
        /// </summary>
        public String UnbundledFolder { get; set; } = "wwwroot/";

        /// <summary>
        /// The name of the bundler minifier core config file to lookup. Default: bundleconfig.json.
        /// </summary>
        [Obsolete("Usage of BundlerMinifierCore is deprecated. Please migrate to artifacts.json.")]
        public String BundlerMinifierCoreFileName { get; set; } = "bundleconfig.json";

        /// <summary>
        /// The name of the artifacts.json file to lookup. Default: artifacts.json.
        /// </summary>
        public String ArtifactsJsonFileName { get; set; } = "artifacts.json";
    }
}
