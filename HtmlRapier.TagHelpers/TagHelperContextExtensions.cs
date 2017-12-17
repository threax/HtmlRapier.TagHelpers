using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public static class TagHelperContextExtensions
    {
        public static String MergeAttribute(this TagHelperContext context, String attrName, String value, String separator = " ")
        {
            var currentValue = context.AllAttributes[attrName]?.Value;
            return $"{value}{separator}{currentValue}";
        }

        public static String MergeClasses(this TagHelperContext context, String value)
        {
            return context.MergeAttribute("class", value);
        }
    }
}
