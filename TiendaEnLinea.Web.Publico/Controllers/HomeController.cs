using AutoMapper;
using Bitworks.Abstract.Notificaciones.Email;
using Bitworks.Notificaciones.Email;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaEnLinea.Core.DTOs.Pedido;
using TiendaEnLinea.Core.DTOs.ProductoPublico;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Services;
using Bitworks.Extensiones.Bytes;
using Bitworks.Extensiones.Imagenes;
using System.Net.Mail;
using System.Net;

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

            Pedido abierto = _pedidoService.GetPedidoAbiertoCliente(id);
            if (abierto == null)
                return RedirectToAction("Index", new {state= "noproducts" });

            //Pedido ped = _pedidoService.GetPedidoNoTracking(new Guid(id));
            //if(ped == null)
            //{

            //    //DeleteCookie();
            //    return RedirectToAction("Index");
            //}

            //if (ped.Completado)
            //{
            //    //DeleteCookie();
            //    return RedirectToAction("Index");

            //}

            return View();
        }

        [HttpGet]
        public ActionResult Preguntas()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GuardarCliente(ClienteDTO cliente)
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
            
            //Pedido ped = _pedidoService.GetPedidoByCliente(cli.Codigo);
            //if (ped == null)
            //{
            //    //inicializo el pedido
            //    Guid nuevo = Guid.NewGuid();
            //    ped = _pedidoService.IniciarPedido(nuevo,cli.Codigo);
                    
            //}
            ////seteo la cookie
            SetCookieCliente(cli.Codigo);
                           
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult VerificarCliente(string id)
        {
            Cliente cli = _pedidoService.GetCliente(id);
            if (cli != null)
            {
                //Pedido ped = _pedidoService.GetPedidoByCliente(cli.Codigo);
                //if (ped == null)
                //{
                //    //inicializo el pedido
                //    Guid nuevo = Guid.NewGuid();
                //    ped = _pedidoService.IniciarPedido(nuevo, cli.Codigo);
                //}
                SetCookieCliente(id);
                return Json(new { nuevo=0, url = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { nuevo = 1 }, JsonRequestBehavior.AllowGet);            
        }

        [HttpGet]
        public ActionResult _Detalle()
        {
            string cliente = GetCookieCliente();
            Pedido abierto = _pedidoService.GetPedidoAbiertoCliente(cliente);

            return View("_Detalle",new CarretillaDTO
            {
                Pedido = _pedidoService.GetPedidoCarretilla(abierto.Codigo)
            });
        }

        
        [HttpGet]
        [OutputCache(Duration = 7200, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult GetMedia(Guid id)
        {
            Multimedia media = _productoService.GetMultimedia(id);
            if (media != null) {
                var r =
                media.Contenido.bwGetImagen().bwCambiarTamanoConRelacionByte(new Size(250, 250), 75, true);
              
                return File(r, r.bwGetMimeType());
                 
                //return File(media.Contenido, media.MimeType, media.NombreArchivo);
            }
            
            else
                throw new HttpException(404, "El archivo seleccionado no existe");

        }

        [HttpPost]
        public ActionResult AgregarProducto(AgregarDTO data)
        {
            string cliente = GetCookieCliente();           
            if(string.IsNullOrEmpty(cliente))
                return Json(new { url=Url.Action("Index") }, JsonRequestBehavior.AllowGet);

            Pedido pe = _pedidoService.GetPedidoAbiertoCliente(cliente);
            if(pe == null)
            {
              pe =  _pedidoService.IniciarPedido(Guid.NewGuid(), cliente);
            }

            //Guid id = new Guid(pedido);
            Producto producto = _productoService.GetProducto(data.IdProducto,false);

            ProductosPedido existente = _pedidoService.GetDetallePedido(pe.Codigo, producto.Codigo);
            if(existente != null)
            {
                decimal nuevaCantidad = existente.Cantidad + data.Cantidad;
                if (nuevaCantidad <= 0)
                    nuevaCantidad = producto.MultiploVenta;

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
                    IdPedido = pe.Codigo,
                    Cantidad = data.Cantidad,
                    SubTotal = GetSubTotal(producto, data.Cantidad),
                    Modificado = false,
                    Precio = producto.PrecioOferta != null ? producto.PrecioOferta.Value : producto.Precio
                });
            }

            _pedidoService.ActualizarTotal(pe.Codigo);
                       
            return Json(new { titulo= "Exito!", mensaje= "El producto fue agregado", tipo= "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ModificarDetalle(AgregarDTO data)
        {
            string cliente = GetCookieCliente();
            if (string.IsNullOrEmpty(cliente))
                return Json(new { url = Url.Action("Index") }, JsonRequestBehavior.AllowGet);


            Pedido abierto = _pedidoService.GetPedidoAbiertoCliente(cliente);
            if(abierto == null)
                return Json(new { url = Url.Action("Index") }, JsonRequestBehavior.AllowGet);

            Producto producto = _productoService.GetProducto(data.IdProducto, false);

            ProductosPedido enBase = _pedidoService.GetDetallePedido(abierto.Codigo, data.IdProducto);     
            enBase.Cantidad = data.Cantidad;
            enBase.SubTotal = GetSubTotal(producto, data.Cantidad);
            enBase.Modificado = true;
            enBase.Precio = producto.PrecioOferta != null ? producto.PrecioOferta.Value : producto.Precio;
            _pedidoService.ModificarDetallePedido(enBase);

            _pedidoService.ActualizarTotal(abierto.Codigo);

            return Json(new { titulo = "Exito!", mensaje = "Pedido modificado", tipo = "success" }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult EliminarDetalle(int codigo)
        {
            string cliente = GetCookieCliente();
            if (string.IsNullOrEmpty(cliente))
                return RedirectToAction("Index");

            Pedido abierto = _pedidoService.GetPedidoAbiertoCliente(cliente);

            ProductosPedido detalle = _pedidoService.GetDetalleByCodigo(codigo);
            if(detalle.IdPedido != abierto.Codigo)
                return Json(new { titulo = "Ops!", mensaje = "Parece que algo salio mal", tipo = "error" }, JsonRequestBehavior.AllowGet);

            _pedidoService.EliminarDetalle(codigo);
            _pedidoService.ActualizarTotal(abierto.Codigo);

            return RedirectToAction("Carretilla");
        }

        [HttpGet]
        public ActionResult FinalizarPedido(string comentario= null)
        {
            
            string id = GetCookieCliente();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");


            Pedido abierto = _pedidoService.GetPedidoAbiertoCliente(id);
            if(abierto == null)
                return RedirectToAction("Index");


            Pedido pedido = _pedidoService.GetPedidoNoTracking(abierto.Codigo,true);
           

            if (pedido.ProductosPedidos == null || pedido.ProductosPedidos.Count == 0)
                return RedirectToAction("Carretilla", new { state = "invalid" });

            pedido.Completado = true;
            pedido.FechaCompletado = DateTime.Now;
            pedido.IdEstado = EstadoPedido.Enviado;
            if (!string.IsNullOrEmpty(comentario))
            {
                pedido.Comentario = Server.UrlDecode(comentario);
            }
            
            pedido.OrdenEntrega = _pedidoService.GetMaxOrderEntrega();
            _pedidoService.ModificarPedido(pedido);

            //DeleteCookie();
            _checkOutService.CrearListaCheckout(pedido.Codigo);
            try
            {
                EnviarNotificacion(pedido);
            }
            catch
            {

            }

           return RedirectToAction("Gracias");
        }

        private void EnviarNotificacion(Pedido pedido)
        {        
            string dominio = System.Configuration.ConfigurationManager.AppSettings["dominio"];
            dominio = dominio + "Pedido/Detalle/" + pedido.Codigo;

            var fromAddress = new MailAddress("dlafincasvstore@gmail.com", "Tienda en línea");
            string fromPassword = "enohp4407$";

            List<MailAddress> destinos = new List<MailAddress>();
            destinos.Add(new MailAddress("stefany.preza@gmail.com", "stefany.preza@gmail.com"));
            destinos.Add(new MailAddress("c.rivas8191@gmail.com", "c.rivas8191@gmail.com"));
            destinos.Add(new MailAddress("julioulloa16@gmail.com", "julioulloa16@gmail.com"));
            

            string subject = "Nuevo pedido";
            string body = "|| Cliente: " + pedido.Cliente.NombreCompleto + " || Contacto: " + pedido.Cliente.Codigo + " || Total: " + pedido.Total.Value.ToString("$0.00") + " || Accede al detalle del pedido aquí: " + dominio + " ||";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            foreach (var item in destinos)
            {
                using (var message = new MailMessage(fromAddress, item)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            
        }

        [HttpGet]
        public ActionResult dummy(Guid id)
        {
            Pedido pedido = _pedidoService.GetPedidoNoTracking(id, true);
            EnviarNotificacion(pedido);
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


            HttpCookie myCookie = Request.Cookies["cliente"];

            if (myCookie != null)
            {
                return myCookie.Value;               
            }
            else
                return null;

        }

        private void SetCookieCliente(string cliente)
        {
            
            HttpCookie myCookie = new HttpCookie("cliente");
            // Set the cookie value.
            myCookie.Value = cliente;
            // Set the cookie expiration date.
            myCookie.Expires = DateTime.Now.AddYears(1);
            // Add the cookie.
            Response.Cookies.Add(myCookie);
        }


        private void DeleteCookie()
        {
            HttpCookie myCookie = Request.Cookies["cliente"];
            myCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(myCookie);
            Response.Cookies.Remove("cliente");
      
        }

        [HttpGet]
        public ActionResult Pedidos(string rango = "")
        {
            string cliente = GetCookieCliente();
            if (string.IsNullOrEmpty(cliente))
                return RedirectToAction("Index");

            return View(_pedidoService.GetResumenPedidos(rango,cliente));
        }

        [HttpGet]
        public ActionResult DetallePedido(Guid idPedido)
        {
            string cliente = GetCookieCliente();

            Pedido ped = _pedidoService.GetPedidoDetalle(idPedido);

            if (ped.IdCliente != cliente)
                return RedirectToAction("Index");

            return View(ped);
        }
    }
}