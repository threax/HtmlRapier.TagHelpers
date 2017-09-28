using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace HtmlRapier.TagHelpers
{
    /// <summary>
    /// This tag helper will serialize a class that implements IClientConfig to the page
    /// as configuration for the client side. What gets serialized depends on the class specified.
    /// Specify the class using dependency injection.
    /// </summary>
    public class ClientConfigTagHelper : TagHelper
    {
        private IClientConfig config;
        private IUrlHelperFactory urlHelperFactory;

        public ClientConfigTagHelper(IUrlHelperFactory urlHelperFactory, IClientConfig config)
        {
            this.config = config;
            this.urlHelperFactory = urlHelperFactory;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public bool AddRunner { get; set; } = true;

        public String RunnerName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            output.TagName = "script";
            output.Attributes.Add("type", "text/javascript");

            //Add the access token path in after getting its content url
            var jObj = JObject.FromObject(config);
            jObj.Add("AccessTokenPath", urlHelper.Content(config.AccessTokenPath));

            //Add the action path as the page base path
            var actionInfo = ViewContext.ActionDescriptor as ControllerActionDescriptor;
            if (actionInfo != null)
            {
                jObj.Add("PageBasePath", urlHelper.Action(actionInfo.ActionName, actionInfo.ControllerName));
            }

            if (AddRunner)
            {
                var runnerName = RunnerName;
                if(runnerName == null)
                {
                    runnerName = Path.ChangeExtension(ViewContext.View.Path, null);
                    if(runnerName[0] == '/')
                    {
                        runnerName = runnerName.Substring(1);
                    }
                }
                output.Attributes.Add("data-hr-run", runnerName);
            }

            //Convert to html this way so we don't escape the settings object.
            var html = String.Format(content, jObj.ToString());
            output.Content.SetHtmlContent(html);
        }

        //0 - json
        private const String content = "window.clientConfig = {0};";
    }
}
