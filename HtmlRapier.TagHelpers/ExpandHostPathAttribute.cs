using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlRapier.TagHelpers
{
    /// <summary>
    /// Adding this attribute to a property in a ClientConfig subclass will make
    /// the url expand to an absolute url if it starts with ~/.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExpandHostPathAttribute : Attribute
    {
    }
}
