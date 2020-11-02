using Bitworks.Seguridad.MVC.Atributos;
using System.Web;
using System.Web.Mvc;

namespace TiendaEnLinea.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MenuValuesAttribute("LaTiendaEnLinea"));
            filters.Add(new BitworksAuthorizeAttribute());
            filters.Add(new Bitworks.Logger.Mvc.FilterAttribute.LogErrores());
        }
    }
}
