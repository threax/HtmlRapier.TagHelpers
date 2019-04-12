using HtmlRapier.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlRapier.TagHelpers
{
    public class ReloginBootstrap4 : IRelogin
    {

        public ReloginBootstrap4()
        {
        }

        public void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", context.MergeClasses("modal fade"));
            output.Attributes.SetAttribute("tabindex", -1);
            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("data-hr-controller", "hr-relogin");
            output.Attributes.SetAttribute("data-hr-toggle", "dialog");
            output.Content.AppendHtml(markup);
        }

        private String markup = @"
        <div class=""modal-dialog modal-lg"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"">Please Login</h5>
                    <button type = ""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>
                </div>
                <div class=""modal-body"">
                    <iframe data-hr-handle=""loginFrame"" width=""100%"" class=""border-0""></iframe>
                </div>
                <div class=""modal-footer"">
                    <button type = ""button"" class=""btn btn-default"" data-dismiss=""modal"">Close</button>
                </div>
            </div>
        </div>
";
    }
}
