using Bitworks.Seguridad.MVC.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaEnLinea.Core.DTOs.Categoria;
using TiendaEnLinea.Core.Model;
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
            List<CategoriaProducto> categorias =
                _categoriaProductoService.GetListaCategorias().OrderBy(x=>x.Nombre).ToList();

            return View(categorias);
        }


        [CaracteristicasAccion("Detalle categoria",false,true)]
        [HttpGet]
        public ActionResult DetalleCategoria(int? id = null)
        {
            CategoriaProductoDTO response = new CategoriaProductoDTO() { Codigo = 0, Activo = true };
            if (id != null) { 
                CategoriaProducto cat = _categoriaProductoService.GetCategoria(id.Value);
                response.Codigo = cat.Codigo;
                response.Nombre = cat.Nombre;
                response.Activo = cat.Activa;
            }           
            return View(response);
        }


        [CaracteristicasAccion("Guardar categoria",false,true)]
        [HttpPost]
        public ActionResult DetalleCategoria(CategoriaProductoDTO data)
        {

            if(data.Codigo != 0)
            {
                _categoriaProductoService.ModificarCategoria(new CategoriaProducto() { 
                    Activa = data.Activo,
                    Codigo = data.Codigo,
                    Nombre = data.Nombre
                });
            }
            else
            {
                _categoriaProductoService.GuardarCategoria(new CategoriaProducto() { 
                    Codigo = 0,
                    Activa = data.Activo,
                    Nombre = data.Nombre
                });
            }
            
            return RedirectToAction("Index");
        }
    }
}