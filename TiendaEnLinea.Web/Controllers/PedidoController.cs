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
        private static IMapper _mapper;
        private IPedidoService _pedidoService;
        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
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
            return View();
        }

    }
}