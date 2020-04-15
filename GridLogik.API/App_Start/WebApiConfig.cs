using GridLogik.API.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GridLogik.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            string allowedOrigins = ConfigurationManager.AppSettings["allowedOrigins"] == null ? "*" : ConfigurationManager.AppSettings["allowedOrigins"].ToString() == "" ? "*" : ConfigurationManager.AppSettings["allowedOrigins"].ToString();
            var cors = new EnableCorsAttribute(allowedOrigins, "*", "*");
            config.EnableCors(cors);
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            
            config.Filters.Add(new ExceptionFilter());
            config.Filters.Add(new UnitOfWorkActionFilter());

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            //config.Formatters.Clear();
            //config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
