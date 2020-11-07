using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.Services
{
    public interface IPedidoService
    {
        List<Pedido> GetPedidosPendientes();

        Pedido IniciarPedido(Guid id);

        Pedido ModificarPedido(Pedido pedido);

        ProductosPedido AgregarDetalle(ProductosPedido productosPedido);
        void EliminarDetalle(int Codigo);
        Pedido GetPedidoNoTracking(Guid id);
        ProductosPedido ModificarDetallePedido(ProductosPedido detalle);

        Pedido GetPedidoCarretilla(Guid idPedido);
    }
}
