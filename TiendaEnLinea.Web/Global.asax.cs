using Bitworks.Diagnostico;
using Bitworks.Seguridad.MVC.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TiendaEnLinea.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var logError = new
            DatosErrorHTTP(Request, Server.GetLastError().GetBaseException());
            logError.PublicarError();          
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
            ConfiguracionSeguridad.SyncSeguridad();

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                ConfiguracionSeguridad.getPrincipalBitworks();
                
            }
        }
    }
}
