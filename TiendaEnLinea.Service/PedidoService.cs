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
        private IClienteRepository _clienteRepository;
        public PedidoService(
            IPedidoRepository pedidoRepository,
            IProductoPedidoRepository productoPedidoRepository,
            IClienteRepository clienteRepository
            ) {
            _pedidoRepository = pedidoRepository;
            _productoPedidoRepository = productoPedidoRepository;
            _clienteRepository = clienteRepository;
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
            return _pedidoRepository.GetLista(x => x.Despachado == false,null,new System.Linq.Expressions.Expression<Func<Pedido, object>>[] { 
                x=>x.Cliente,
                x=>x.ProductosPedidos
            }).OrderBy(x=>x.FechaIngreso).ToList();
        }

        public Pedido IniciarPedido(Guid id,string idCliente)
        {
            return _pedidoRepository.Inicializar(new Pedido() { 
                Codigo= id,
                Completado = false,
                IdCliente = idCliente,
                IdEstado = EstadoPedido.Abierto
            });
        }

        public Pedido ModificarPedido(Pedido pedido)
        {
            return _pedidoRepository.Modificar(pedido);
        }

        public Pedido GetPedidoNoTracking(Guid id,bool incluirDetalle = false)
        {
            if(incluirDetalle)
                return _pedidoRepository.FindBy(x => x.Codigo == id,new System.Linq.Expressions.Expression<Func<Pedido, object>>[] {x=>x.ProductosPedidos });
            else
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

        public ProductosPedido GetDetallePedido(Guid idPedido,Guid idProducto) {
            return _productoPedidoRepository.FindBy(x => x.IdPedido == idPedido && x.IdProducto == idProducto);
        }

        public ProductosPedido GetDetalleByCodigo(int codigo)
        {
            return _productoPedidoRepository.FindBy(x => x.Codigo == codigo);
        }

        public Cliente GetCliente(string id)
        {
            return _clienteRepository.FindBy(x => x.Codigo == id);
        }

        public Pedido GetPedidoByCliente(string id)
        {
            return _pedidoRepository.FindBy(x => x.IdCliente == id && x.Completado == false);
        }

        public Cliente GuardarCliente(Cliente cliente)
        {
            return _clienteRepository.GuardarCliente(cliente);
        }
    }
}
