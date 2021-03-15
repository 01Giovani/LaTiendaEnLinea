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
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        private TiendaEnLineaContext _db;
        public ProductoRepository(BaseContext context) : base(context)
        {
            _db = context as TiendaEnLineaContext;
        }

        public Producto GuardarProducto(Producto producto)
        {
            Producto remoto = _db.Producto.FirstOrDefault(x => x.Codigo == producto.Codigo);
            if (remoto == null)
                _db.Producto.Add(producto);
            else {
                remoto.Nombre = producto.Nombre;
                remoto.Descripcion = producto.Descripcion;
                remoto.Precio = producto.Precio;
                remoto.PrecioOferta = producto.PrecioOferta;
                remoto.Destacado = producto.Destacado;
                remoto.Activo = producto.Activo;
                remoto.MultiploVenta = producto.MultiploVenta;
                remoto.PrefijoVenta = producto.PrefijoVenta;
                remoto.DescripcionOferta = producto.DescripcionOferta;
                remoto.IdCategoria = producto.IdCategoria;
            }

            _db.SaveChanges();


            return remoto ?? producto;
        }
    }
}
