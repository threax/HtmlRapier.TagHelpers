using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public class PageNumbersTagHelper : TagHelper
    {
        private IPageNumbers pageNumbers;

        public PageNumbersTagHelper(IPageNumbers pageNumbers)
        {
            this.pageNumbers = pageNumbers;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.pageNumbers.Process(context, output, NumPageNumbers, ItemsPerPageOptions, ControllerName);
        }

        public int NumPageNumbers { get; set; } = 7;

        public String ItemsPerPageOptions { get; set; } = "5, 10, 20, 50, 100";

        public String ControllerName { get; set; } = "pageNumbers";
    }
}
