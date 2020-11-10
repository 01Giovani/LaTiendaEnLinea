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
    public class CheckoutRepository : GenericRepository<CheckOut>, ICheckoutRepository
    {
        private TiendaEnLineaContext _db;
        public CheckoutRepository(BaseContext context) : base(context)
        {
            _db = context as TiendaEnLineaContext;
        }

        public void GuardarLista(List<CheckOut> detalles)
        {
            _db.CheckOut.AddRange(detalles);
            _db.SaveChanges();
        }
    }
}
