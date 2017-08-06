using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public class LoadTagHelper : ToggleTagHelper
    {
        public LoadTagHelper()
            :base(openMarkup: OpenMarkup, closeMarkup: CloseMarkup)
        {
            this.HrToggle = "load";
        }

        private const string OpenMarkup = @"
        <div class=""center"">
            <div class=""load-linebar center-block"">
                <div class=""rect1""></div>
                <div class=""rect2""></div>
                <div class=""rect3""></div>
                <div class=""rect4""></div>
                <div class=""rect5""></div>
            </div>";

        private const string CloseMarkup = "</div>";
    }
}
