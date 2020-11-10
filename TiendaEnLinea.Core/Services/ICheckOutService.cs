using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.Services
{
    public interface ICheckOutService
    {
        void CrearListaCheckout(Guid idPedido);
    }
}
