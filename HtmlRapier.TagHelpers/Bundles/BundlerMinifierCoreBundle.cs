using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HtmlRapier.TagHelpers
{
    class BundlerMinifierCoreBundle
    {
        [JsonPropertyName("outputFileName")]
        public string OutputFileName { get; set; }

        [JsonPropertyName("inputFiles")]
        public List<string> InputFiles { get; set; } = new List<string>();
    }
}
