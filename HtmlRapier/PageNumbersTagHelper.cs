using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    public class PageNumbersTagHelper : TagHelper
    {
        private static readonly char[] Seps = new char[] { ',' };

        public PageNumbersTagHelper()
        {

        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add(new TagHelperAttribute("data-hr-controller", ControllerName));
            output.Attributes.SetAttribute("class", context.MergeClasses("clearfix"));
            output.PreContent.AppendHtml(StartContent);

            for (int i = 0; i < NumPageNumbers; ++i)
            {
                output.PreContent.AppendHtml(String.Format(PageLine, i));
            }
            output.PostContent.AppendHtml(EndPageNumbers);

            var perPageStrings = ItemsPerPageOptions.Split(Seps, StringSplitOptions.RemoveEmptyEntries);
            foreach (var perPage in perPageStrings.Select(i => i.Trim()))
            {
                output.PostContent.AppendHtml(String.Format(OptionLine, perPage));
            }

            output.PostContent.AppendHtml(End);
        }

        public int NumPageNumbers { get; set; } = 7;

        public String ItemsPerPageOptions { get; set; } = "5, 10, 20, 50, 100";

        public String ControllerName { get; set; } = "pageNumbers";

        private const String StartContent = @"
            <div data-hr-model=""totals"">Items {{itemStart}} - {{itemEnd}} of {{total}}</div>
            <ul class=""pagination pull-left"">
                <li data-hr-toggle=""first"" aria-label=""First"" data-hr-on-click=""pageFirst"" data-hr-class-off=""disabled"">
                    <a href=""#First"">
                        <span aria-hidden=""true"" class=""glyphicon glyphicon-backward""></span>
                    </a>
                </li>
                <li data-hr-toggle=""previous"" aria-label=""Previous"" data-hr-on-click=""pagePrevious"" data-hr-class-off=""disabled"">
                    <a href=""#Previous"">
                        <span aria-hidden=""true"" class=""glyphicon glyphicon-triangle-left""></span>
                    </a>
                </li>";

        private const String PageLine = @"
                <li data-hr-toggle=""page{0}"" data-hr-model=""page{0}"" data-hr-class-active=""active"" data-hr-style-off=""display:none;"" data-hr-on-click=""page{0}""><a href=""#Page"">{{{{pageNum}}}}</a></li>";

        private const String EndPageNumbers = @"<li data-hr-toggle=""next"" aria-label=""Next"" data-hr-on-click=""pageNext"" data-hr-class-off=""disabled"">
                    <a href=""#Next"">
                        <span aria-hidden=""true"" class=""glyphicon glyphicon-triangle-right""></span>
                    </a>
                </li>
                <li data-hr-toggle=""last"" aria-label=""Last"" data-hr-on-click=""pageLast"" data-hr-class-off=""disabled"">
                    <a href=""#Last"">
                        <span aria-hidden=""true"" class=""glyphicon glyphicon-forward""></span>
                    </a>
                </li>
            </ul>
            <div class=""clearfix""></div>
            <form class=""form-inline pull-left"" data-hr-model=""itemsPerPage"" data-hr-on-change=""itemsPerPageChanged"">
                <div class=""form-group"">
                    <label for=""itemsPerPage"">Items per Page</label>
                    <select name=""itemsPerPage"" class=""form-control"">";

        private const String OptionLine = @"
                        <option value=""{0}"">{0}</option>";

        private const String End = @"
                    </select>
                </div>
            </form>";
    }
}
