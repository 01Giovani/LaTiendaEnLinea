using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.Services
{
    public interface IProductoService
    {
        List<Producto> GetProductos();
        Producto GetProducto(Guid id, bool includes = true);
        Producto GuardarProducto(Producto producto);

        void EliminarImagen(int id);
        MediasProducto GuardarImagen(MediasProducto data);

        Multimedia GuardarMultimedia(Multimedia multimedia);
        Multimedia GetMultimedia(Guid id);

        List<Producto> GetProductosTienda(string palabra);
    }
}
