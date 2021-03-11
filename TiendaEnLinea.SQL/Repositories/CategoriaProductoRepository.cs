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
    public class CategoriaProductoRepository:GenericRepository<CategoriaProducto>, ICategoriaProductoRepository
    {
        private TiendaEnLineaContext _db;
        public CategoriaProductoRepository(BaseContext baseContext):base(baseContext)
        {
            _db = baseContext as TiendaEnLineaContext;
        }

        public CategoriaProducto GuardarCategoria(CategoriaProducto categoria)
        {
            _db.CategoriaProducto.Add(categoria);
            _db.SaveChanges();

            return categoria;
        }

        public CategoriaProducto ModificarCategoria(CategoriaProducto categoria)
        {
            CategoriaProducto tracking = _db.CategoriaProducto.FirstOrDefault(x => x.Codigo == categoria.Codigo);
            tracking.Nombre = categoria.Nombre;
            tracking.Activa = categoria.Activa;

            _db.SaveChanges();

            return tracking;
        }
    }
}
