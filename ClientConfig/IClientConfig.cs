using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    /// <summary>
    /// This interface makes a class able to provide the page config for the ClientConfigTagHelper.
    /// It contains no interface since it is just serialized, but this way DI can figure it out.
    /// Classes that implement this interface should not store secrets since all of it is serialized
    /// to the client.
    /// This class also reserves the name PageBasePath, so don't use that in a subclass.
    /// </summary>
    public interface IClientConfig
    {
        /// <summary>
        /// This is a reserved property, it won't actually be accessed, but will be calculated and sent to the client
        /// side as part of the object.
        /// </summary>
        [JsonIgnore]
        String PageBasePath { get; }
    }
}
