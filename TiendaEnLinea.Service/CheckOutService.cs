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
    public class CheckOutService: ICheckOutService
    {
        private IPedidoRepository _pedidoRepository;
        private ICheckoutRepository _checkoutRepository;

        public CheckOutService(
            IPedidoRepository pedidoRepository,
            ICheckoutRepository checkoutRepository
            )
        {
            _pedidoRepository = pedidoRepository;
            _checkoutRepository = checkoutRepository;
        }
        public void CrearListaCheckout(Guid idPedido)
        {
            Pedido pedido = _pedidoRepository.FindBy(x => x.Codigo == idPedido,
                new System.Linq.Expressions.Expression<Func<Pedido, object>>[] { x=>x.ProductosPedidos });

            List<CheckOut> pedidoDetalles = new List<CheckOut>();
            foreach (var item in pedido.ProductosPedidos)
            {
                pedidoDetalles.Add(new CheckOut() { 
                    IdPedido = pedido.Codigo,
                    IdDetallePedido = item.Codigo,
                    Preparado = false,
                    Comentario = null
                });
            }

            _checkoutRepository.GuardarLista(pedidoDetalles);

        }
    }
}
