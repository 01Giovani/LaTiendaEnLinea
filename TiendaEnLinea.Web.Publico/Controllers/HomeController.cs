using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaEnLinea.Core.DTOs.Pedido;
using TiendaEnLinea.Core.DTOs.ProductoPublico;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Web.Publico.Controllers
{
    public class HomeController : Controller
    {
        private static IMapper _mapper;
        private IProductoService _productoService;
        private IPedidoService _pedidoService;
        public HomeController(
            IProductoService productoService,
            IPedidoService pedidoService
            )
        {
            _productoService = productoService;
            _pedidoService = pedidoService;
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
        public ActionResult Carretilla()
        {
            string pedido = GetCookieCliente();

            return View(new CarretillaDTO { 
                Pedido = _pedidoService.GetPedidoCarretilla(new Guid(pedido))
            });
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

        [HttpPost]
        public ActionResult AgregarProducto(AgregarDTO data)
        {
            string pedido = GetCookieCliente();
            if (pedido == null)
            {
                Guid nuevo = Guid.NewGuid();
                _pedidoService.IniciarPedido(nuevo);
                SetCookieCliente(nuevo);
                pedido = nuevo.ToString();
            }

            Guid id = new Guid(pedido);
            Producto producto = _productoService.GetProducto(data.IdProducto,false);

            _pedidoService.AgregarDetalle(new ProductosPedido() { 
                IdProducto = data.IdProducto,
                IdPedido = id,
                Cantidad = data.Cantidad,
                SubTotal =  GetSubTotal(producto,data.Cantidad),
                Modificado = false                
            });
            
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ModificarDetalle(AgregarDTO data)
        {
            string pedido = GetCookieCliente();
            
            Guid id = new Guid(pedido);
            Producto producto = _productoService.GetProducto(data.IdProducto, false);

            _pedidoService.ModificarDetallePedido(new ProductosPedido()
            {
                Codigo = data.codigoDetalle.Value,
                IdProducto = producto.Codigo,
                IdPedido = id,
                Cantidad = data.Cantidad,
                SubTotal = GetSubTotal(producto, data.Cantidad),
                Modificado = true
            });

            return Json("ok", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult EliminarDetalle(int codigo)
        {
            _pedidoService.EliminarDetalle(codigo);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FinalizarPedido(Guid id)
        {
            Pedido pedido = _pedidoService.GetPedidoNoTracking(id);
            pedido.Completado = true;
            pedido.FechaCompletado = DateTime.Now;

            _pedidoService.ModificarPedido(pedido);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }


        private decimal GetSubTotal(Producto producto, int cantidad)
        {           
            if (producto.PrecioOferta != null)
            {
                return producto.PrecioOferta.Value * cantidad;
            }
            else {
                return producto.Precio * cantidad;
            }           
        }

        private string GetCookieCliente()
        {


            HttpCookie myCookie = Request.Cookies["pedido"];

            if (myCookie != null)
            {
                return myCookie.Value;               
            }
            else
                return null;

        }

        private void SetCookieCliente(Guid pedido)
        {


            HttpCookie myCookie = new HttpCookie("pedido");
            // Set the cookie value.
            myCookie.Value = pedido.ToString();
            // Set the cookie expiration date.
            myCookie.Expires = DateTime.Now.AddYears(1);
            // Add the cookie.
            Response.Cookies.Add(myCookie);
        }
    }
}