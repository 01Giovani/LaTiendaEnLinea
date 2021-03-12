using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Repositories;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Service
{
    public class CategoriaProductoService: ICategoriaProductoService
    {
        private ICategoriaProductoRepository _categoriaProductoRepository;
        public CategoriaProductoService(
            ICategoriaProductoRepository categoriaProductoRepository
            )
        {
            _categoriaProductoRepository = categoriaProductoRepository;
        }

        public List<CategoriaProducto> GetListaCategoriasSelect()
        {
            return _categoriaProductoRepository.GetLista(x => x.Activa == true);
        }

        public List<CategoriaProducto> GetListaCategorias()
        {
            return _categoriaProductoRepository.GetLista(x => true); 
        }

        public CategoriaProducto GuardarCategoria(CategoriaProducto categoriaProducto)
        {
            return _categoriaProductoRepository.GuardarCategoria(categoriaProducto);
        }

        public CategoriaProducto ModificarCategoria(CategoriaProducto categoriaProducto)
        {
            return _categoriaProductoRepository.ModificarCategoria(categoriaProducto);
        }

        public CategoriaProducto GetCategoria(int id)
        {
            return _categoriaProductoRepository.FindBy(x => x.Codigo == id);
        }
    }
}
