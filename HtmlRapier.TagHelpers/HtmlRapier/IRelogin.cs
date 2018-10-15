using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmlRapier.TagHelpers
{
    public interface IRelogin
    {
        void Process(TagHelperContext context, TagHelperOutput output);
    }
}