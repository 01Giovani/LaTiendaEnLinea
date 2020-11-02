using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaEnLinea.Core.DTOs.ProductoPublico;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Web.Publico.Controllers
{
    public class HomeController : Controller
    {
        private static IMapper _mapper;
        private IProductoService _productoService;
        public HomeController(
            IProductoService productoService
            )
        {
            _productoService = productoService;
        }

        static HomeController()
        {

            var config = new AutoMapper.MapperConfiguration(x =>
            {
                x.CreateMap<Producto, ProductoCatalogoDTO>()
                .ForMember(c=>c.Fotos,c=>c.MapFrom(o=>o.Multimedias.Select(v=>v.IdMultimedia).ToList()));
            });

            _mapper = config.CreateMapper();
        }

        [HttpGet]
        public ActionResult Index(string busqueda= null)
        {
            ViewBag.TextoBuscador = busqueda;

            List<Producto> productos = _productoService.GetProductosTienda(busqueda);

            List<ProductoCatalogoDTO> model = _mapper.Map<List<ProductoCatalogoDTO>>(productos);

            return View(model);
        }


        
        [HttpGet]
        [OutputCache(Duration = 7200, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult GetMedia(Guid id)
        {
            Multimedia media = _productoService.GetMultimedia(id);
            if (media != null)
                return File(media.Contenido, media.MimeType, media.NombreArchivo);
            else
                throw new HttpException(404, "El archivo seleccionado no existe");

        }
    }
}