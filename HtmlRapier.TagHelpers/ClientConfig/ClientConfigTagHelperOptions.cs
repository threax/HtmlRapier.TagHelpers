using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlRapier.TagHelpers.ClientConfig
{
    public class ClientConfigTagHelperOptions
    {
        /// <summary>
        /// A list of route args that should be cleared when writing out PageBasePaths to the client config.
        /// Defaults to null, which means none.
        /// </summary>
        public List<String> RouteArgsToClear { get; set; }
    }
}
