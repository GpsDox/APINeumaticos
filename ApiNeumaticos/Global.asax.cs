using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System;

namespace ApiNeumaticos
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    //to get requested domain from request header
        //    var requestedDomain = Request.ServerVariables["HTTP_ORIGIN"];
        //    var isEnabledDomain = false;
        //    if (requestedDomain != null)
        //    {
        //        var enabledDomains = "http://testsite.com, https://testsite.com";
        //        foreach (var enabledDomain in enabledDomains.Split(','))
        //        {
        //            if (enabledDomain.Equals(requestedDomain))
        //            {
        //                isEnabledDomain = true;
        //                break;
        //            }
        //        }
        //        if (isEnabledDomain)
        //        {
        //            HttpContext context = HttpContext.Current;
        //            HttpResponse response = context.Response;

        //            response.AddHeader("Access-Control-Allow-Origin", requestedDomain);
        //            response.AddHeader("X-Frame-Options", "ALLOW-FROM *");
        //            if (context.Request.HttpMethod == "OPTIONS")
        //            {
        //                response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
        //                response.AddHeader("Access-Control-Allow-Headers", "authorization, content-type");
        //                response.AddHeader("Access-Control-Allow-Credentials", "true");
        //                response.AddHeader("Access-Control-Max-Age", "1728000");
        //                response.End();
        //            }
        //        }
        //    }
        //}


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            //Evito las referencias circulares al trabajar con Entity FrameWork         
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            //Elimino que el sistema devuelva en XML, sólo trabajaremos con JSON
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);


        }
    }
}
