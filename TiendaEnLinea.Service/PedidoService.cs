using System;
using System.Collections.Generic;
using System.Linq;
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
            )
        {
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

        public List<Pedido> GetPedidosAdmin(DateTime fi, DateTime ff, EstadoPedido? idEstado = null)
        {
            return _pedidoRepository.GetLista(x => (x.FechaIngreso <= ff && x.FechaIngreso >= fi) && (idEstado == null || x.IdEstado == idEstado),
                null,
                new System.Linq.Expressions.Expression<Func<Pedido, object>>[] {
                x=>x.Cliente,
                x=>x.ProductosPedidos
            }).OrderBy(x =>x.OrdenEntrega ?? 1000).ToList();
        }

        public Pedido IniciarPedido(Guid id, string idCliente)
        {
            return _pedidoRepository.Inicializar(new Pedido()
            {
                Codigo = id,
                Completado = false,
                IdCliente = idCliente,
                IdEstado = EstadoPedido.Abierto,
                FechaIngreso = DateTime.Now
            });
        }

        public Pedido ModificarPedido(Pedido pedido)
        {
            return _pedidoRepository.Modificar(pedido);
        }

        public Pedido GetPedidoNoTracking(Guid id, bool incluirDetalle = false)
        {
            if (incluirDetalle)
                return _pedidoRepository.FindBy(x => x.Codigo == id, new System.Linq.Expressions.Expression<Func<Pedido, object>>[] { x => x.ProductosPedidos });
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

        public ProductosPedido GetDetallePedido(Guid idPedido, Guid idProducto)
        {
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

        public Pedido GetPedidoDetalle(Guid id)
        {
            return _pedidoRepository.FindBy(x => x.Codigo == id, new System.Linq.Expressions.Expression<Func<Pedido, object>>[] {
                x=>x.Cliente,
                x=>x.ProductosPedidos.Select(c=>c.Producto.Multimedias)
            });
        }

        public void ActualizarTotal(Guid idPedido)
        {
            decimal total = _productoPedidoRepository.GetLista(x => x.IdPedido == idPedido).Sum(x => x.SubTotal);
            Pedido pedido = _pedidoRepository.FindByTracking(x => x.Codigo == idPedido);
            pedido.Total = total;
            _pedidoRepository.SaveChanges();
        }

        public Pedido GetSiguientePreparar()
        {
            return _pedidoRepository
                .GetLista(x => x.IdEstado == EstadoPedido.Enviado && x.FechaCompletado != null)
                .OrderBy(x => x.OrdenEntrega ?? 10000).FirstOrDefault();
        }

        public List<Pedido> GetPedidosNoEntregados()
        {
            return _pedidoRepository.GetLista(X => X.IdEstado == EstadoPedido.Preparado, null,
                new System.Linq.Expressions.Expression<Func<Pedido, object>>[] {
                    x=>x.Cliente,
                    x=>x.ProductosPedidos.Select(c=>c.Producto)
                });
        }


        public int GetMaxOrderEntrega()
        {
            return _pedidoRepository.GeTMaxOrden();
        }

        public void CambiarOrderPedido(Guid idPedido,int orden)
        {
            int orderViejo;
            if (orden < 1)
                orden = 1; 
            Pedido pedidoACambiar = _pedidoRepository.FindByTracking(x => x.Codigo == idPedido);
            orderViejo = pedidoACambiar.OrdenEntrega.Value;
            pedidoACambiar.OrdenEntrega = orden;

            Pedido pedidoReem = _pedidoRepository.FindByTracking(x => (x.IdEstado == EstadoPedido.Preparado || x.IdEstado == EstadoPedido.Enviado) && x.OrdenEntrega == orden);
            if(pedidoReem != null)
                pedidoReem.OrdenEntrega = orderViejo;

            _pedidoRepository.SaveChanges();

        }

        public Pedido GetPedidoAbiertoCliente(string idCliente)
        {
            return _pedidoRepository.FindBy(x => x.IdEstado == EstadoPedido.Abierto && x.IdCliente == idCliente);
        }

        public List<Pedido> GetResumenPedidos(string rango,string idCliente)
        {
            switch (rango)
            {
                case "mes":
                    DateTime toda = DateTime.Today;
                    DateTime parametro = new DateTime(toda.Year, toda.Month, 1);
                    return _pedidoRepository.GetLista(x => x.IdEstado != EstadoPedido.Abierto && x.IdCliente == idCliente && x.FechaCompletado >= parametro);
                    
                case "mesanterior":
                    DateTime parametro2 = DateTime.Today.AddMonths(-1);
                    return _pedidoRepository.GetLista(x => x.IdEstado != EstadoPedido.Abierto && x.IdCliente == idCliente && x.FechaCompletado >= parametro2);
                case "tresmeses":
                    DateTime parametro3 = DateTime.Today.AddMonths(-3);
                    return _pedidoRepository.GetLista(x => x.IdEstado != EstadoPedido.Abierto && x.IdCliente == idCliente && x.FechaCompletado >= parametro3);
                default:
                    DateTime toda1 = DateTime.Today;
                    DateTime parametro4 = new DateTime(toda1.Year, toda1.Month, 1);
                    return _pedidoRepository.GetLista(x => x.IdEstado != EstadoPedido.Abierto && x.IdCliente == idCliente && x.FechaCompletado >= parametro4);
            }
        }
    }
}
