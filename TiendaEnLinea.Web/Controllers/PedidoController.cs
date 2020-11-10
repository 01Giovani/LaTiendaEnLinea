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

        [CaracteristicasAccion("pedidos pendientes preparar",false,true)]
        [HttpGet]
        public ActionResult PendientePreparar()
        {

            List<Pedido> pedidos = _pedidoService.GetPedidosPendientes();
            

            return View("Index",pedidos);
        }

        [CaracteristicasAccion("pedidos preparados", false, true)]
        [HttpGet]
        public ActionResult Preparados()
        {

            List<Pedido> pedidos = _pedidoService.GetPedidosPendientes();


            return View(pedidos);
        }



        [CaracteristicasAccion("pedidos no enviados por cliente", false, true)]
        [HttpGet]
        public ActionResult NoEnviados()
        {

            List<Pedido> pedidos = _pedidoService.GetPedidosPendientes();


            return View(pedidos);
        }

    
        [CaracteristicasAccion("Lista de pedidos entregados", false, true)]
        [HttpGet]
        public ActionResult Entregados()
        {

            List<Pedido> pedidos = _pedidoService.GetPedidosPendientes();


            return View(pedidos);
        }

    }
}