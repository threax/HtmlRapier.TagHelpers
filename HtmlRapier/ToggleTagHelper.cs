using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public class ToggleTagHelper : TagHelper
    {
        private String tagName;
        private String onStyle;
        private String openMarkup;
        private String closeMarkup;
        private String hiddenStartClass;

        public ToggleTagHelper(String tagName = "div", String onStyle = "display:block;", String hiddenStartClass = "hiddenToggler", String openMarkup = null, String closeMarkup = null)
        {
            this.tagName = tagName;
            this.onStyle = onStyle;
            this.openMarkup = openMarkup;
            this.closeMarkup = closeMarkup;
            this.hiddenStartClass = hiddenStartClass;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (openMarkup != null)
            {
                output.PreContent.AppendHtml(openMarkup);
            }

            if (closeMarkup != null)
            {
                output.PostContent.AppendHtml(closeMarkup);
            }

            output.TagName = tagName;
            output.Attributes.SetAttribute("data-hr-toggle", HrToggle);

            if (Visible)
            {
                output.Attributes.SetAttribute("data-hr-style-off", "display:none;");
            }
            else
            {
                var classes = context.AllAttributes["class"]?.Value;
                output.Attributes.SetAttribute("class", context.MergeClasses(hiddenStartClass));
                output.Attributes.SetAttribute("data-hr-style-on", onStyle);
            }
        }

        public String HrToggle { get; set; }

        public bool Visible { get; set; } = false;
    }
}
