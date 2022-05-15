using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EzDeliveryAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var corattr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corattr);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            GlobalConfiguration.Configuration.MessageHandlers.Add(new APIKeyHandler());
            config.Filters.Add(new EzDeliveryExceptionAttribute());
        }



    }


}
