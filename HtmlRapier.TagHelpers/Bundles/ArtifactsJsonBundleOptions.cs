using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HtmlRapier.TagHelpers
{
    class ArtifactsJsonBundleOptions
    {
        [JsonPropertyName("input")]
        public List<String> Input { get; set; }

        [JsonPropertyName("out")]
        public String Out { get; set; }
    }
}
