using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.Services
{
    public interface ICheckOutService
    {
        void CrearListaCheckout(Guid idPedido);

        List<CheckOut> GetPedidoCheckout(Guid idPedido);
        void ModificarDetalle(int codigo, bool preparado, string comentario);
    }
}
