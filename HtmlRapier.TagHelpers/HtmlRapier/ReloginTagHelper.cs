using HtmlRapier.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlRapier.TagHelpers
{
    public class ReloginTagHelper : TagHelper
    {

        public ReloginTagHelper()
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", context.MergeClasses("modal fade"));
            output.Attributes.SetAttribute("tabindex", -1);
            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("data-hr-controller", "hr-relogin");
            output.Attributes.SetAttribute("data-hr-toggle", "dialog");
            output.Attributes.SetAttribute("style", context.MergeAttribute("style", "z-index:1400", ";"));
            output.Content.AppendHtml(markup);
        }

        private String markup = @"
        <div class=""modal-dialog modal-lg"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <button type = ""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>
                    <h4 class=""modal-title"">Please Login</h4>
                </div>
                <div class=""modal-body"">
                    <iframe data-hr-handle=""loginFrame"" width=""100%"" style=""border:0px none""></iframe>
                </div>
                <div class=""modal-footer"">
                    <button type = ""button"" class=""btn btn-default"" data-dismiss=""modal"">Close</button>
                </div>
            </div>
        </div>
";
    }
}
