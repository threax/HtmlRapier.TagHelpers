using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public class ErrorTagHelper : ToggleTagHelper
    {
        public ErrorTagHelper()
        {
            this.HrToggle = "error";
        }
    }
}
