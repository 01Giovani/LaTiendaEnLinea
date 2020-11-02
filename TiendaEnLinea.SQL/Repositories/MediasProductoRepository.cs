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
    public class MediasProductoRepository : GenericRepository<MediasProducto>, IMediasProductoRepository
    {
        private TiendaEnLineaContext _db;
        public MediasProductoRepository(BaseContext context) : base(context)
        {
            _db = context as TiendaEnLineaContext;
        }

        public MediasProducto GuardarImagen(MediasProducto data)
        {
            _db.MediasProducto.Add(data);
            _db.SaveChanges();
            return data;
        }

        public void EliminarImagen(int id)
        {
            MediasProducto file = _db.MediasProducto.FirstOrDefault(x => x.Codigo == id);
            if(file != null)
            {
                _db.MediasProducto.Remove(file);
                _db.SaveChanges();

            }
        } 

    }
}
