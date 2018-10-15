using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public class ModalTagHelper : TagHelper
    {
        private IModal modal;

        public ModalTagHelper(IModal modal)
        {
            this.modal = modal;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            modal.Process(context, output, HrToggle, AddHeader, TitleText, DialogClasses);
        }

        public String HrToggle { get; set; } = "dialog";

        public bool AddHeader { get; set; } = true;

        public String TitleText { get; set; } //1

        public String DialogClasses { get; set; } = "";
    }
}
