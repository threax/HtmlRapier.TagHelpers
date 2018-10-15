using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public class ModalBootstrap3 : IModal
    {
        public ModalBootstrap3()
        {

        }

        public void Process(TagHelperContext context, TagHelperOutput output, String hrToggle, bool addHeader, String titleText, String dialogClasses)
        {
            var modalGuid = Guid.NewGuid().ToString();

            output.TagName = "div";

            output.PreContent.AppendHtml(String.Format(PreContent, dialogClasses != null && dialogClasses != "" ? " " + dialogClasses : ""));
            if (addHeader)
            {
                output.PreContent.AppendHtml(String.Format(HeaderContent, modalGuid, titleText));
                output.Attributes.SetAttribute("aria-labelledby", modalGuid);
            }
            output.PostContent.AppendHtml(PostContent);

            output.Attributes.SetAttribute("class", context.MergeClasses("modal fade"));
            output.Attributes.Add("tabindex", "-1");
            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("data-hr-toggle", hrToggle);
        }

        private const String PreContent = @"<div class=""modal-dialog{0}"" role=""document"">
        <div class=""modal-content"">";

        //0 - modalGuid
        //1 - TitleText
        private const String HeaderContent = @"<div class=""modal-header"">
            <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>
            <h4 class=""modal-title"" id=""{0}"">{1}</h4>
        </div>";

        private const String PostContent = @"</div>
        </div>";
    }
}
