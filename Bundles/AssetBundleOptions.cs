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
    }
}
