using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Repositories;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Service
{
    public class ClienteService:IClienteService
    {
        private IClienteRepository _clienteRepository;
        public ClienteService(
            IClienteRepository clienteRepository
            )
        {
            _clienteRepository = clienteRepository;
        }


        public List<Cliente> GetListaClientes()
        {
            return _clienteRepository.GetLista(null).OrderBy(x => x.NombreCompleto).ToList() ;
        }

        public Cliente GetCliente(string id)
        {
            return _clienteRepository.FindBy(x => x.Codigo == id);
        }

        public void GuardarCliente(Cliente cliente)
        {
            _clienteRepository.ModificarCliente(cliente);
        }
    }
}
