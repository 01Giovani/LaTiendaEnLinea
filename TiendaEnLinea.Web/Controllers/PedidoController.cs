using AutoMapper;
using Bitworks.Seguridad.MVC.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaEnLinea.Core.DTOs.Pedido;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Web.Controllers
{
    public class PedidoController : Controller
    {
        private ICheckOutService _checkOutService;
        private static IMapper _mapper;
        private IPedidoService _pedidoService;
        public PedidoController(IPedidoService pedidoService,
            ICheckOutService checkOutService)
        {
            _pedidoService = pedidoService;
            _checkOutService = checkOutService;
        }

        static PedidoController()
        {
            var config = new AutoMapper.MapperConfiguration(x =>
            {
                x.CreateMap<Pedido, ListaPedidosDTO>();
                

            });

            _mapper = config.CreateMapper();

        }

        [CaracteristicasAccion("Listado de pedidos",false,true)]
        [HttpGet]
        public ActionResult Listado(string fi,string ff,string estado)
        {
            DateTime fInicio;
            DateTime fFIn;
            

            if (string.IsNullOrEmpty(fi))            
                fInicio = DateTime.Today.AddMonths(-1);            
            else 
                fInicio = DateTime.Parse(fi).AddMonths(-1);
            

            if (string.IsNullOrEmpty(ff))            
                fFIn = DateTime.Today.AddDays(1).AddSeconds(-1);            
            else            
                fFIn = DateTime.Parse(ff).AddDays(1).AddSeconds(-1);


            EstadoPedido? idEstado = null;

            if (string.IsNullOrEmpty(estado))
                estado = "2";

            if (estado != "-1")
                idEstado = (EstadoPedido)int.Parse(estado);

            List<Pedido> pedidos = _pedidoService.GetPedidosAdmin(fInicio, fFIn, idEstado);

            ViewBag.fi = fInicio.ToString("dd/MM/yyyy");
            ViewBag.ff = fFIn.ToString("dd/MM/yyyy");
            ViewBag.estado = estado;

            return View("Index",pedidos);
        }

        [CaracteristicasAccion("Detalle pedido",false,true)]
        [HttpGet]
        public ActionResult Detalle(Guid id)
        {

            Pedido pedido = _pedidoService.GetPedidoDetalle(id);

            return View(pedido);
        }


        [CaracteristicasAccion("Preparar pedido", false, true)]
        [HttpGet]
        public ActionResult Preparacion(Guid id)
        {
            Pedido pedido = _pedidoService.GetPedidoDetalle(id);
            if (pedido.IdEstado != EstadoPedido.Enviado)
                return RedirectToAction("Listado");

            CheckoutListaDTO model = new CheckoutListaDTO() { 
                Pedido = pedido,
                Detalles = _checkOutService.GetPedidoCheckout(id)
            };

            return View(model);
        }

        [CaracteristicasAccion("Marcar preaparado",false,true)]
        [HttpPost]
        public ActionResult Preparado(PreracionDTO data)
        {
            _checkOutService.ModificarDetalle(data.IdDetalle, data.Preparado, data.Comentario);

            return Json(new { titulo = "Exito!", mensaje = "Se marco como preparado", tipo = "success" }, JsonRequestBehavior.AllowGet);
        }

        [CaracteristicasAccion("Marcar pedido como pendiente entrega",false,true)]
        [HttpGet]
        public ActionResult PedidoPreparado(Guid idPedido)
        {

            Pedido ped = _pedidoService.GetPedidoNoTracking(idPedido);
            ped.IdEstado = EstadoPedido.Preparado;
            _pedidoService.ModificarPedido(ped);

            Pedido siguiente = _pedidoService.GetSiguientePreparar();

            if (siguiente != null)
                return RedirectToAction("Preparacion",new { id = siguiente.Codigo });
            else
                return RedirectToAction("Listado");
            
        }


        [CaracteristicasAccion("Cancelar pedido",false,true)]
        [HttpGet]
        public ActionResult Cancelar(Guid id)
        {

            Pedido pedido = _pedidoService.GetPedidoNoTracking(id);
            pedido.IdEstado = EstadoPedido.NoEntregado;
            _pedidoService.ModificarPedido(pedido);

            return RedirectToAction("Detalle", new { id });
        }

    }
}