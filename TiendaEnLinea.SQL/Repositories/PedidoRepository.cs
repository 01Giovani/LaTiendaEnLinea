using Bitworks.Data;
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

            _db.SaveChanges();

            return remoto;
        }
    }
}
