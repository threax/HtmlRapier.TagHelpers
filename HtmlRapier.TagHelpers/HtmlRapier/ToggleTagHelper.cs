using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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

            if (!String.IsNullOrWhiteSpace(AriaLiveMessage))
            {
                output.Attributes.SetAttribute("aria-live", "off");
                if (AriaLiveMessageBeforeElement)
                {
                    CreateLiveMessage(output.PreElement);
                }
                else
                {
                    CreateLiveMessage(output.PostElement);
                }
            }
        }

        /// <summary>
        /// Set the name of the toggle.
        /// </summary>
        public String HrToggle { get; set; }

        /// <summary>
        /// Set whether the toggle is visible when it starts. Defaults to false.
        /// </summary>
        public bool Visible { get; set; } = false;

        /// <summary>
        /// Set this to a message to have that read instead of the element content if this toggle is used
        /// in an aria-live region. This will create an extra toggle element only for screen readers with the
        /// message in it. The aria-live attribute will be set to off for the main content.
        /// </summary>
        public String AriaLiveMessage { get; set; }

        /// <summary>
        /// Set the location of the AriaLiveMessage. True (default) will put the message element before
        /// the main element and false will put it after.
        /// </summary>
        public bool AriaLiveMessageBeforeElement { get; set; } = true;

        /// <summary>
        /// The name of the class to use for the aria live message element. Defaults to "sr-only".
        /// </summary>
        public String AriaLiveMessageSrClass { get; set; } = "sr-only";

        protected void CreateLiveMessage(TagHelperContent content)
        {
            var classes = WebUtility.HtmlEncode(AriaLiveMessageSrClass);
            content.AppendHtml($@"<{WebUtility.HtmlEncode(tagName)} data-hr-toggle=""{WebUtility.HtmlEncode(HrToggle)}""");
            if (Visible)
            {
                content.AppendHtml($@" data-hr-style-off=""display:none;""");
            }
            else
            {
                classes += " " + WebUtility.HtmlEncode(hiddenStartClass);
                content.AppendHtml($@" data-hr-style-on=""{WebUtility.HtmlEncode(onStyle)}""");
            }
            content.AppendHtml($@" class=""{classes}"">{WebUtility.HtmlEncode(AriaLiveMessage)}</{WebUtility.HtmlEncode(tagName)}>");
        }
    }
}
