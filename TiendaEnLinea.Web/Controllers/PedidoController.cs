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

        [CaracteristicasAccion("Lista de pedidos",false,true)]
        [HttpGet]
        public ActionResult Index()
        {

            List<Pedido> pedidos = _pedidoService.GetPedidosPendientes();
            List<ListaPedidosDTO> model = _mapper.Map<List<ListaPedidosDTO>>(pedidos);

            return View(model);
        }
    }
}