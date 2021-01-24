using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.Services
{
    public interface IClienteService
    {
        List<Cliente> GetListaClientes();
        Cliente GetCliente(string id);
        void GuardarCliente(Cliente cliente);
    }
}
