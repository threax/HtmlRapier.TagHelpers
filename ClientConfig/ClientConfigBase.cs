using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlRapier.TagHelpers
{
    /// <summary>
    /// Abstract base class for client configs.
    /// </summary>
    public abstract class ClientConfigBase : IClientConfig
    {
        public String PageBasePath => null;
    }
}
