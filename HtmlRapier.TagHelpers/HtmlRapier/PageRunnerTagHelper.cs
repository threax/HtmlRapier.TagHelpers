using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.IO;

namespace HtmlRapier.TagHelpers
{
    /// <summary>
    /// This will add a span with data-hr-run set to the current page or a specified runner name.
    /// </summary>
    public class PageRunnerTagHelper : TagHelper
    {
        private readonly string tagName;

        public PageRunnerTagHelper(String tagName = "span")
        {
            this.tagName = tagName;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = tagName;
            var runnerName = Path.ChangeExtension(ViewContext.View.Path, null);
            if (runnerName[0] == '/')
            {
                runnerName = runnerName.Substring(1);
            }
            output.Attributes.Add("data-hr-run", runnerName);
        }
    }
}
