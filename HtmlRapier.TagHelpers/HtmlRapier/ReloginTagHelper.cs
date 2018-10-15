using HtmlRapier.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlRapier.TagHelpers
{
    public class ReloginTagHelper : TagHelper
    {
        private IRelogin relogin;

        public ReloginTagHelper(IRelogin relogin)
        {
            this.relogin = relogin;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.relogin.Process(context, output);
        }
    }
}
