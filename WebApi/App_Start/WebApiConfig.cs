using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var formatters = config.Formatters;
            var jsonFormatter = formatters.JsonFormatter;

            // remove xml formatter
            formatters.Remove(formatters.XmlFormatter);

            // json referenceloop
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            // json referenceloop objects for testing / otherwise none
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;

            // pretty output
            jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            // response case
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // ignore null objects
            jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            // helps with serializing references to other objects when dealing with EF POCO references
            // GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            //// change the DateTime format
            //jsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

            //// TimeZone format?
            //jsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            //// culture of the serializer
            //jsonFormatter.SerializerSettings.Culture = new CultureInfo("et-EE");

            // wildcards only for development ease
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "AsunikAPI",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
