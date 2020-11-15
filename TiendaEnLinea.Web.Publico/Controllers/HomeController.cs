using AutoMapper;
using Bitworks.Abstract.Notificaciones.Email;
using Bitworks.Notificaciones.Email;
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
        private ICheckOutService _checkOutService;
        public HomeController(
            IProductoService productoService,
            IPedidoService pedidoService,
            ICheckOutService checkOutService
            )
        {
            _productoService = productoService;
            _pedidoService = pedidoService;
            _checkOutService = checkOutService;
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
        public ActionResult Gracias()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Carretilla()
        {

            string id = GetCookieCliente();
            if (id == null)
                return RedirectToAction("Index");

            Pedido ped = _pedidoService.GetPedidoNoTracking(new Guid(id));
            if(ped == null)
            {

                DeleteCookie();
                return RedirectToAction("Index");
            }

            if (ped.Completado)
            {
                DeleteCookie();
                return RedirectToAction("Index");

            }

            return View();
        }

        [HttpGet]
        public ActionResult Preguntas()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CheckClient(ClienteDTO cliente)
        {

            Cliente cli = _pedidoService.GetCliente(cliente.Telefono);
            if(cli == null)
            {
                cli = _pedidoService.GuardarCliente(new Cliente() { 
                    Codigo =cliente.Telefono,
                    NombreCompleto = cliente.Nombre,
                    Telefono = cliente.Telefono,
                    Direccion = cliente.Direccion
                });
            }
            
            Pedido ped = _pedidoService.GetPedidoByCliente(cli.Codigo);
            if (ped == null)
            {
                //inicializo el pedido
                Guid nuevo = Guid.NewGuid();
                ped = _pedidoService.IniciarPedido(nuevo,cli.Codigo);
                    
            }
            //seteo la cookie
            SetCookieCliente(ped.Codigo);
                           
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult VerificarCliente(string id)
        {
            Cliente cli = _pedidoService.GetCliente(id);
            if (cli != null)
            {
                Pedido ped = _pedidoService.GetPedidoByCliente(cli.Codigo);
                if (ped == null)
                {
                    //inicializo el pedido
                    Guid nuevo = Guid.NewGuid();
                    ped = _pedidoService.IniciarPedido(nuevo, cli.Codigo);
                }
                SetCookieCliente(ped.Codigo);
                return Json(new { nuevo=0, url = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { nuevo = 1 }, JsonRequestBehavior.AllowGet);            
        }

        [HttpGet]
        public ActionResult _Detalle()
        {
            string pedido = GetCookieCliente();

            return View("_Detalle",new CarretillaDTO
            {
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
            if(string.IsNullOrEmpty(pedido))
                return Json(new { url=Url.Action("Index") }, JsonRequestBehavior.AllowGet);


            Guid id = new Guid(pedido);
            Producto producto = _productoService.GetProducto(data.IdProducto,false);

            ProductosPedido existente = _pedidoService.GetDetallePedido(id, producto.Codigo);
            if(existente != null)
            {
                decimal nuevaCantidad = existente.Cantidad + data.Cantidad;
                if (nuevaCantidad <= 0)
                    nuevaCantidad = 1;

                existente.Precio = producto.PrecioOferta != null ? producto.PrecioOferta.Value : producto.Precio;
                existente.SubTotal = GetSubTotal(producto, nuevaCantidad);
                existente.Cantidad = nuevaCantidad;
                existente.Modificado = true;
                _pedidoService.ModificarDetallePedido(existente);
            }
            else
            {
                existente = _pedidoService.AgregarDetalle(new ProductosPedido()
                {
                    IdProducto = data.IdProducto,
                    IdPedido = id,
                    Cantidad = data.Cantidad,
                    SubTotal = GetSubTotal(producto, data.Cantidad),
                    Modificado = false,
                    Precio = producto.PrecioOferta != null ? producto.PrecioOferta.Value : producto.Precio
                });
            }

            _pedidoService.ActualizarTotal(id);
                       
            return Json(new { titulo= "Exito!", mensaje= "El producto fue agregado", tipo= "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ModificarDetalle(AgregarDTO data)
        {
            string pedido = GetCookieCliente();
            if (string.IsNullOrEmpty(pedido))
                return Json(new { url = Url.Action("Index") }, JsonRequestBehavior.AllowGet);

            Guid id = new Guid(pedido);
            
            Producto producto = _productoService.GetProducto(data.IdProducto, false);

            ProductosPedido enBase = _pedidoService.GetDetallePedido(id, data.IdProducto);     
            enBase.Cantidad = data.Cantidad;
            enBase.SubTotal = GetSubTotal(producto, data.Cantidad);
            enBase.Modificado = true;
            enBase.Precio = producto.PrecioOferta != null ? producto.PrecioOferta.Value : producto.Precio;
            _pedidoService.ModificarDetallePedido(enBase);

            _pedidoService.ActualizarTotal(id);

            return Json(new { titulo = "Exito!", mensaje = "Pedido modificado", tipo = "success" }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult EliminarDetalle(int codigo)
        {
            string pedido = GetCookieCliente();
            if (string.IsNullOrEmpty(pedido))
                return RedirectToAction("Index");

            Guid id = new Guid(pedido);

            ProductosPedido detalle = _pedidoService.GetDetalleByCodigo(codigo);
            if(detalle.IdPedido != id)
                return Json(new { titulo = "Ops!", mensaje = "Parece que algo salio mal", tipo = "error" }, JsonRequestBehavior.AllowGet);

            _pedidoService.EliminarDetalle(codigo);
            _pedidoService.ActualizarTotal(id);

            return RedirectToAction("Carretilla");
        }

        [HttpGet]
        public ActionResult FinalizarPedido()
        {
            
            Guid id = new Guid(GetCookieCliente());
            if (id == null)
                return RedirectToAction("Index");

            Pedido pedido = _pedidoService.GetPedidoNoTracking(id,true);
            if(pedido != null && pedido.IdEstado == EstadoPedido.Enviado)
                return RedirectToAction("Index");

            if (pedido.ProductosPedidos == null || pedido.ProductosPedidos.Count == 0)
                return RedirectToAction("Carretilla", new { state = "invalid" });

            pedido.Completado = true;
            pedido.FechaCompletado = DateTime.Now;
            pedido.IdEstado = EstadoPedido.Enviado;
            pedido.OrdenEntrega = _pedidoService.GetMaxOrderEntrega();
            _pedidoService.ModificarPedido(pedido);

            //DeleteCookie();
            _checkOutService.CrearListaCheckout(pedido.Codigo);
            try
            {
                EnviarNotificacion(pedido.Codigo);
            }
            catch
            {

            }

           return RedirectToAction("Gracias");
        }


        private void EnviarNotificacion(Guid id)
        {
            NotificacionEmailService noti = new NotificacionEmailService(null);

            string origen = System.Configuration.ConfigurationManager.AppSettings["correoOrigen"];
            string correo = System.Configuration.ConfigurationManager.AppSettings["destinoNotificacion"];
            string dominio = System.Configuration.ConfigurationManager.AppSettings["dominio"];
            dominio = dominio + "Pedido/Detalle/" + id;

            noti.PrepararEnvio(new DireccionEmail("Tienda en linea", origen),
            new DireccionEmail("Admin", correo), "Nuevo pedido", "Accede al detalle del pedido aquí: "+dominio );
            noti.enviarMensaje();
        }

        [HttpGet]
        public ActionResult dummy()
        {
            EnviarNotificacion(Guid.NewGuid());
            return RedirectToAction("Index");
        }

        private decimal GetSubTotal(Producto producto, decimal cantidad)
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


        private void DeleteCookie()
        {
            HttpCookie myCookie = Request.Cookies["pedido"];
            myCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(myCookie);
            Response.Cookies.Remove("pedido");
      
        }
    }
}