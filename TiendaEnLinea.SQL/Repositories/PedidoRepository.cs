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
    }
}
