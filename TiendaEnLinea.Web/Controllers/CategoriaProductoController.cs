using Bitworks.Seguridad.MVC.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaEnLinea.Core.DTOs.Categoria;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Web.Controllers
{
    public class CategoriaProductoController : Controller
    {

        private ICategoriaProductoService _categoriaProductoService;
        public CategoriaProductoController(
            ICategoriaProductoService categoriaProductoService
            )
        {
            _categoriaProductoService = categoriaProductoService;
        }
        
        [CaracteristicasAccion("lista categorias",false,true)]
        public ActionResult Index()
        {
            return View();
        }


        [CaracteristicasAccion("Detalle categoria",false,true)]
        [HttpGet]
        public ActionResult DetalleCategoria(int? id = null)
        {

            return View();
        }


        [CaracteristicasAccion("Guardar categoria",false,true)]
        [HttpPost]
        public ActionResult DetalleCategoria(CategoriaProductoDTO data)
        {
            return RedirectToAction("Index");
        }
    }
}