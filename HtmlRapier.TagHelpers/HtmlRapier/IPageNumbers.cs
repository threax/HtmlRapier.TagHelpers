using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmlRapier.TagHelpers
{
    public interface IPageNumbers
    {
        void Process(TagHelperContext context, TagHelperOutput output, int numPageNumbers, string itemsPerPageOptions, string controllerName);
    }
}