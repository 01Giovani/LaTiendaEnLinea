using Bitworks.Seguridad.MVC.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaEnLinea.Web.Controllers
{
    public class HomeController : Controller
    {
        [CaracteristicasAccion("Index", false, true)]
        public ActionResult Index()
        {
            return View();
        }

    
    }
}