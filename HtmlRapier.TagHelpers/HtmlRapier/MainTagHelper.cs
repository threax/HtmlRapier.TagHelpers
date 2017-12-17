using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public class MainTagHelper : ToggleTagHelper
    {
        public MainTagHelper()
        {
            this.HrToggle = "main";
        }
    }
}
