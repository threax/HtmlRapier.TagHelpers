﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public class PageNumbersBootstrap3 : IPageNumbers
    {
        private static readonly char[] Seps = new char[] { ',' };

        public PageNumbersBootstrap3()
        {

        }

        public void Process(TagHelperContext context, TagHelperOutput output, int numPageNumbers, String itemsPerPageOptions, String startItemsPerPage, String controllerName)
        {
            output.TagName = "nav";
            output.Attributes.Add(new TagHelperAttribute("data-hr-controller", controllerName));
            output.Attributes.SetAttribute("class", context.MergeClasses("clearfix"));
            if (!output.Attributes.ContainsName("aria-label"))
            {
                output.Attributes.SetAttribute("aria-label", "pages");
            }
            if(startItemsPerPage != null)
            {
                output.Attributes.SetAttribute("data-hr-config-startitemsperpage", startItemsPerPage);
            }
            output.PreContent.AppendHtml(StartContent);

            for (int i = 0; i < numPageNumbers; ++i)
            {
                output.PreContent.AppendHtml(String.Format(PageLine, i));
            }
            output.PostContent.AppendHtml(EndPageNumbers);

            var perPageStrings = itemsPerPageOptions.Split(Seps, StringSplitOptions.RemoveEmptyEntries);
            foreach (var perPage in perPageStrings.Select(i => i.Trim()))
            {
                output.PostContent.AppendHtml(String.Format(OptionLine, perPage));
            }

            output.PostContent.AppendHtml(End);
        }

        private const String StartContent = @"
            <div data-hr-model=""totals"">Items {{itemStart}} - {{itemEnd}} of {{total}}</div>
            <ul class=""pagination pull-left"">
                <li data-hr-toggle=""first"" data-hr-on-click=""pageFirst"" data-hr-class-off=""disabled"">
                    <a href=""#"" aria-label=""First"">
                        <span aria-hidden=""true"" class=""glyphicon glyphicon-backward""></span>
                    </a>
                </li>
                <li data-hr-toggle=""previous"" data-hr-on-click=""pagePrevious"" data-hr-class-off=""disabled"">
                    <a href=""#"" aria-label=""Previous"">
                        <span aria-hidden=""true"" class=""glyphicon glyphicon-triangle-left""></span>
                    </a>
                </li>";

        private const String PageLine = @"
                <li data-hr-toggle=""page{0}"" data-hr-model=""page{0}"" data-hr-class-active=""active"" data-hr-style-off=""display:none;"" data-hr-on-click=""page{0}""><a href=""#"">{{{{pageNum}}}}</a></li>";

        private const String EndPageNumbers = @"<li data-hr-toggle=""next"" data-hr-on-click=""pageNext"" data-hr-class-off=""disabled"">
                    <a href=""#"" aria-label=""Next"">
                        <span aria-hidden=""true"" class=""glyphicon glyphicon-triangle-right""></span>
                    </a>
                </li>
                <li data-hr-toggle=""last"" data-hr-on-click=""pageLast"" data-hr-class-off=""disabled"">
                    <a href=""#"" aria-label=""Last"">
                        <span aria-hidden=""true"" class=""glyphicon glyphicon-forward""></span>
                    </a>
                </li>
            </ul>
            <div class=""clearfix""></div>
            <form class=""form-inline pull-left"" data-hr-model=""itemsPerPage"" data-hr-on-change=""itemsPerPageChanged"">
                <div class=""form-group"">
                    <label for=""itemsPerPage"">Items per Page</label>
                    <select name=""itemsPerPage"" class=""form-control"" aria-label=""Items Per Page"">";

        private const String OptionLine = @"
                        <option value=""{0}"">{0}</option>";

        private const String End = @"
                    </select>
                </div>
            </form>";
    }
}
