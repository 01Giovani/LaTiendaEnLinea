using Bitworks.Seguridad.MVC.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaEnLinea.Core.DTOs.Dashboard;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Web.Controllers
{
    public class DashboardController : Controller
    {
        private IDashboardService _dashboardService;
        public DashboardController(
            IDashboardService dashboardService
            )
        {
            _dashboardService = dashboardService;
        }

        // GET: Dashboard
        [CaracteristicasAccion("Dashboard actividad",false,true)]
        public ActionResult Index()
        {
            DashboardDTO response = _dashboardService.GetDataDashboard();

            return View(response);
        }
    }
}