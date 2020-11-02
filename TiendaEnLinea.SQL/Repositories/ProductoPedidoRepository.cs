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
    public class ProductoPedidoRepository : GenericRepository<ProductosPedido>, IProductoPedidoRepository
    {
        private TiendaEnLineaContext _db;
        public ProductoPedidoRepository(BaseContext context) : base(context)
        {
            _db = context as TiendaEnLineaContext;
        }
    }
}
