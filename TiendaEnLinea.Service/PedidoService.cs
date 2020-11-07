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
    public class PedidoService : IPedidoService
    {
        private IPedidoRepository _pedidoRepository;
        private IProductoPedidoRepository _productoPedidoRepository;
        public PedidoService(
            IPedidoRepository pedidoRepository,
            IProductoPedidoRepository productoPedidoRepository
            ) {
            _pedidoRepository = pedidoRepository;
            _productoPedidoRepository = productoPedidoRepository;
        }

        public ProductosPedido AgregarDetalle(ProductosPedido productosPedido)
        {
            return _productoPedidoRepository.Agregar(productosPedido);
        }

        public void EliminarDetalle(int Codigo)
        {
            _productoPedidoRepository.Eliminar(Codigo);
        }

        public List<Pedido> GetPedidosPendientes()
        {
            return _pedidoRepository.GetLista(x => x.Despachado == false).OrderBy(x=>x.FechaIngreso).ToList();
        }

        public Pedido IniciarPedido(Guid id)
        {
            return _pedidoRepository.Inicializar(new Pedido() { 
                Codigo= id,
                Completado = false
            });
        }

        public Pedido ModificarPedido(Pedido pedido)
        {
            return _pedidoRepository.Modificar(pedido);
        }

        public Pedido GetPedidoNoTracking(Guid id)
        {
            return _pedidoRepository.FindBy(x => x.Codigo == id);
        }

        public ProductosPedido ModificarDetallePedido(ProductosPedido detalle)
        {
            return _productoPedidoRepository.Modificar(detalle);
        }

        public Pedido GetPedidoCarretilla(Guid idPedido)
        {
            return _pedidoRepository.FindBy(x => x.Codigo == idPedido, new System.Linq.Expressions.Expression<Func<Pedido, object>>[] { 
                x=>x.ProductosPedidos.Select(c=>c.Producto.Multimedias)            
            });
        }
    }
}
