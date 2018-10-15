using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmlRapier.TagHelpers
{
    public interface IModal
    {
        void Process(TagHelperContext context, TagHelperOutput output, string hrToggle, bool addHeader, string titleText, string dialogClasses);
    }
}