﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.Services
{
    public interface IPedidoService
    {
        List<Pedido> GetPedidosAdmin(DateTime fi, DateTime ff, EstadoPedido? idEstado = null);

        Pedido IniciarPedido(Guid id, string idCliente);

        Pedido ModificarPedido(Pedido pedido);

        ProductosPedido AgregarDetalle(ProductosPedido productosPedido);
        void EliminarDetalle(int Codigo);
        Pedido GetPedidoNoTracking(Guid id, bool incluirDetalle = false);
        ProductosPedido ModificarDetallePedido(ProductosPedido detalle);

        Pedido GetPedidoCarretilla(Guid idPedido);

        ProductosPedido GetDetallePedido(Guid idPedido, Guid idProducto);

        ProductosPedido GetDetalleByCodigo(int codigo);

        Cliente GetCliente(string id);
        Pedido GetPedidoByCliente(string id);

        Cliente GuardarCliente(Cliente cliente);

        Pedido GetPedidoDetalle(Guid id);

        void ActualizarTotal(Guid idPedido);

        Pedido GetSiguientePreparar();
        List<Pedido> GetPedidosNoEntregados();

        int GetMaxOrderEntrega();
        void CambiarOrderPedido(Guid idPedido, int orden);

        Pedido GetPedidoAbiertoCliente(string idCliente);

        List<Pedido> GetResumenPedidos(string rango, string idCliente);
    }
}
