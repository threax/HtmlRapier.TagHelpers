﻿using HtmlRapier.TagHelpers.ClientConfig;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        private ClientConfigTagHelperOptions options;

        public ClientConfigTagHelper(IUrlHelperFactory urlHelperFactory, IClientConfig config, ClientConfigTagHelperOptions options)
        {
            this.config = config;
            this.urlHelperFactory = urlHelperFactory;
            this.options = options;
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

            //Add the action path as the page base path
            var actionInfo = ViewContext.ActionDescriptor as ControllerActionDescriptor;
            if (actionInfo != null)
            {
                var valueDictionary = new Dictionary<String, Object>();
                if (options.RouteArgsToClear != null)
                {
                    foreach (var option in options.RouteArgsToClear)
                    {
                        valueDictionary.Add(option, "");
                    }
                }

                var urlActionContext = new UrlActionContext()
                {
                    Action = actionInfo.ActionName,
                    Controller = actionInfo.ControllerName,
                    Values = valueDictionary
                };

                jObj.Add("PageBasePath", urlHelper.Action(urlActionContext));
            }

            var request = ViewContext.HttpContext.Request;

            foreach(var prop in config.GetType().GetProperties().Where(i => i.GetCustomAttributes(typeof(ExpandHostPathAttribute), true).Any()))
            {
                var path = jObj[prop.Name]?.ToString();
                if (path.StartsWith("~/"))
                {
                    jObj[prop.Name] = $"https://{request.Host.Value}{urlHelper.Content(path)}";
                }
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
        private const String content =
@"window.hr_config = (function(next){{
    return function(config)
    {{
        config.client = {0};
        return next ? next(config) : config;
    }}
}})(window.hr_config);";
    }
}
