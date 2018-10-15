using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlRapier.TagHelpers
{
    public enum FrontEndLibrary
    {
        Bootstrap3,
        Bootstrap4
    }

    /// <summary>
    /// Options for the spc tag helpers.
    /// </summary>
    public class HtmlRapierTagHelperOptions
    {
        public FrontEndLibrary FrontEndLibrary { get; set; } = FrontEndLibrary.Bootstrap3;
    }
}
