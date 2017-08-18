using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public class ModalTagHelper : TagHelper
    {
        public ModalTagHelper()
        {

        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var modalGuid = Guid.NewGuid().ToString();

            output.TagName = "div";

            output.PreContent.AppendHtml(String.Format(PreContent, DialogClasses != null && DialogClasses != "" ? " " + DialogClasses : ""));
            if (AddHeader)
            {
                output.PreContent.AppendHtml(String.Format(HeaderContent, modalGuid, TitleText));
                output.Attributes.SetAttribute("aria-labelledby", modalGuid);
            }
            output.PostContent.AppendHtml(PostContent);

            output.Attributes.SetAttribute("class", context.MergeClasses("modal fade"));
            output.Attributes.Add("tabindex", "-1");
            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("data-hr-toggle", HrToggle);
        }

        public String HrToggle { get; set; } = "dialog";

        public bool AddHeader { get; set; } = true;

        public String TitleText { get; set; } //1

        public String DialogClasses { get; set; } = ""; //PreContent - 0

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
