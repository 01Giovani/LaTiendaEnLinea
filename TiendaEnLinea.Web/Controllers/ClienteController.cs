using AutoMapper;
using Bitworks.Seguridad.MVC.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaEnLinea.Core.DTOs.Cliente;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Web.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteService _clienteService;
        private static IMapper _mapper;
        public ClienteController(
            IClienteService clienteService
            )
        {
            _clienteService = clienteService;
        }

        static ClienteController()
        {
            var config = new AutoMapper.MapperConfiguration(x =>
            {
                x.CreateMap<Cliente, ClienteDTO>();
                x.CreateMap<Cliente, ClienteInDTO>();
                x.CreateMap<ClienteInDTO, Cliente>();
            });

            _mapper = config.CreateMapper();
        }

        [CaracteristicasAccion("Lista cliente",false,true)]
        [HttpGet]
        public ActionResult Index()
        {
            List<Cliente> clientes = _clienteService.GetListaClientes();

            return View(_mapper.Map<List<ClienteDTO>>(clientes));
        }

        [CaracteristicasAccion("Detalle cliente",false,true)]
        [HttpGet]
        public ActionResult Detalle(string id)
        {
            Cliente cl = _clienteService.GetCliente(id);
            ClienteInDTO model = _mapper.Map<ClienteInDTO>(cl);

            return View(model);
        }

        [CaracteristicasAccion("Guardar cliente",false,true)]
        [HttpPost]
        public ActionResult Detalle(ClienteInDTO cliente)
        {
            Cliente cl = _mapper.Map<Cliente>(cliente);
            _clienteService.GuardarCliente(cl);

            return RedirectToAction("Detalle",new { id = cliente.Codigo });
        }


    }
}