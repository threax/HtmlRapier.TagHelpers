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
    /// </summary>
    public interface IClientConfig
    {
        /// <summary>
        /// The path to the access token.
        /// </summary>
        [JsonIgnore]
        String AccessTokenPath { get; }
    }
}
