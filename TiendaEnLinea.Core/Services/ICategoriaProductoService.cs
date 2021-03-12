using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.Services
{
    public interface ICategoriaProductoService
    {
        CategoriaProducto ModificarCategoria(CategoriaProducto categoriaProducto);
        CategoriaProducto GuardarCategoria(CategoriaProducto categoriaProducto);
        List<CategoriaProducto> GetListaCategorias();
        CategoriaProducto GetCategoria(int id);
        List<CategoriaProducto> GetListaCategoriasSelect();
    }
}
