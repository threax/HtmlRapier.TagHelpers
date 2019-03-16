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
            this.pageNumbers.Process(context, output, NumPageNumbers, ItemsPerPageOptions, StartItemsPerPage, ControllerName);
        }

        /// <summary>
        /// The number of page number buttons to create. Default: 7.
        /// </summary>
        public int NumPageNumbers { get; set; } = 7;

        /// <summary>
        /// A comma separated list of the numbers to display in the page options. Default: 5, 10, 20, 50, 100
        /// </summary>
        public String ItemsPerPageOptions { get; set; } = "5, 10, 20, 50, 100";

        /// <summary>
        /// If not null the number of items to show on a page when no value is set. Default: null
        /// </summary>
        public String StartItemsPerPage { get; set; }

        /// <summary>
        /// The name of the controller to find on the page to use for the page numbers.
        /// </summary>
        public String ControllerName { get; set; } = "pageNumbers";
    }
}
