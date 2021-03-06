﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public class ItemEditFormTagHelper : TagHelper
    {
        public ItemEditFormTagHelper()
        {

        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "form";
            output.Attributes.Add(new TagHelperAttribute(Binding, Function));
            output.Attributes.Add(new TagHelperAttribute("data-hr-form", Name));
            output.PreContent.AppendHtml(String.Format(StartContent, Text, ToggleName));
            if (DynamicAfterStatic)
            {
                output.PostContent.AppendHtml(DynamicContent);
            }
            else
            {
                output.PreContent.AppendHtml(DynamicContent);
            }
            output.PostContent.AppendHtml(String.Format(EndContent, Text, ToggleName));
        }

        /// <summary>
        /// The html rapier binding for the submit action, defaults to "data-hr-on-submit".
        /// </summary>
        public String Binding { get; set; } = "data-hr-on-submit";

        /// <summary>
        /// The javascript function to bind the binding to, defaults to "submit"
        /// </summary>
        public String Function { get; set; } = "submit";

        /// <summary>
        /// The text to put on the submit button, defaults to "Save changes".
        /// </summary>
        public String Text { get; set; } = "Save changes"; //0

        public String Name { get; set; } = "input";

        public String ToggleName { get; set; } = "mainError"; //1

        /// <summary>
        /// Set this to true to put dynamic content after static, false to put it before.
        /// </summary>
        public bool DynamicAfterStatic { get; set; } = true;

        private const String StartContent = @"
            <fieldset class=""modal-body"" data-hr-toggle=""viewOnly"" data-hr-disabled-on=""true"">
                <div class=""alert alert-danger hiddenToggler"" role=""alert"" data-hr-toggle=""{1}"" data-hr-view=""{1}"" data-hr-style-on=""display:block;"">{{{{message}}}}</div>
";

        private const String DynamicContent = "<span data-hr-form-end></span>";

        private const String EndContent = @"
            </fieldset>
            <div class=""modal-footer"" data-hr-toggle=""viewOnly"" data-hr-style-on=""display:none;"">
                <button type = ""submit"" class=""btn btn-primary"">{0}</button>
            </div>";
    }
}
