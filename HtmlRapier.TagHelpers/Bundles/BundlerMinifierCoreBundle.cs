using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlRapier.TagHelpers
{
    class BundlerMinifierCoreBundle
    {
        [JsonProperty("outputFileName")]
        public string OutputFileName { get; set; }

        [JsonProperty("inputFiles")]
        public List<string> InputFiles { get; set; } = new List<string>();
    }
}
