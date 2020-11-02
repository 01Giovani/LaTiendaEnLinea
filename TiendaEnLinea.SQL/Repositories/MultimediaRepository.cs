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
    public class MultimediaRepository : GenericRepository<Multimedia>, IMultimediaRepository
    {
        private TiendaEnLineaContext _db;
        public MultimediaRepository(BaseContext context) : base(context)
        {
            _db = context as TiendaEnLineaContext;
        }

        public Multimedia GuardarMultimedia(Multimedia multimedia)
        {
            _db.Multimedia.Add(multimedia);
            _db.SaveChanges();
            return multimedia;
        }
    }
}
