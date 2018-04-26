using System;
using System.Web.Http;

namespace PCWCodeExamplesCSharpGeocoding
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

			// This ensure JSON is returned to browsers by default unless XML is specifically requested
			GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
			.Add(new System.Net.Http.Formatting.RequestHeaderMapping(
				"Accept", 
				"text/html",
				StringComparison.InvariantCultureIgnoreCase,
				true, 
				"application/json"
			));

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}