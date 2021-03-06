﻿using Bitworks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Repositories;

namespace TiendaEnLinea.SQL.Repositories
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        private TiendaEnLineaContext _db;
        public PedidoRepository(BaseContext context) : base(context)
        {
            _db = context as TiendaEnLineaContext;
        }

        public Pedido Inicializar(Pedido pedido)
        {
            _db.Pedido.Add(pedido);
            _db.SaveChanges();
            return pedido;
        }

        public Pedido Modificar(Pedido pedido)
        {
            Pedido remoto = _db.Pedido.FirstOrDefault(x => x.Codigo == pedido.Codigo);

            remoto.IdCliente = pedido.IdCliente;
            remoto.IdBeneficiario = pedido.IdBeneficiario;
            remoto.Total = pedido.Total;
            remoto.Despachado = pedido.Despachado;
            remoto.Comentario = pedido.Comentario;
            remoto.FechaEntregado = pedido.FechaEntregado;
            remoto.OrdenEntrega = pedido.OrdenEntrega;
            remoto.Completado = pedido.Completado;
            remoto.FechaCompletado = pedido.FechaCompletado;
            remoto.IdEstado = pedido.IdEstado;
            remoto.IdTipoPago = pedido.IdTipoPago;

            _db.SaveChanges();

            return remoto;
        }

        public int GeTMaxOrden()
        {
            return _db.Pedido.Where(x=>x.IdEstado == EstadoPedido.Enviado).Max(x => x.OrdenEntrega).GetValueOrDefault(0) +1;
        }

        public decimal GetTotalPedidosMes()
        {
            DateTimeOffset ahora = DateTimeOffset.Now;

            decimal? res = _db.Pedido
                .Where(x =>
                x.IdEstado == EstadoPedido.Entregado &&
                x.FechaIngreso.Month == ahora.Month &&
                x.FechaIngreso.Year == ahora.Year).Sum(x => x.Total);

            return res ?? 0; ;
        }
    }
}
