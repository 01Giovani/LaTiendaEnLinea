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
    public class ProductoService: IProductoService
    {
        private IProductoRepository _productoRepository;
        private IMediasProductoRepository _mediasProductoRepository;
        private IMultimediaRepository _multimediaRepository;
        public ProductoService(
            IProductoRepository productoRepository,
            IMediasProductoRepository mediasProductoRepository,
            IMultimediaRepository multimediaRepository
            ) {
            _productoRepository = productoRepository;
            _mediasProductoRepository = mediasProductoRepository;
            _multimediaRepository = multimediaRepository;
        }

        public List<Producto> GetProductos()
        {
            return _productoRepository.GetLista(x => true,null,new System.Linq.Expressions.Expression<Func<Producto, object>>[] {x=>x.Multimedias });
        }

        public Producto GetProducto(Guid id,bool includes = true)
        {

            List<System.Linq.Expressions.Expression<Func<Producto, object>>> inc = new List<System.Linq.Expressions.Expression<Func<Producto, object>>>();

            if (includes)
                inc.Add(x => x.Multimedias);


            return _productoRepository.FindBy(x => x.Codigo == id, inc.ToArray());
        }

        public Producto GuardarProducto(Producto producto)
        {
            return _productoRepository.GuardarProducto(producto);
        }

        public void EliminarImagen(int id)
        {
            _mediasProductoRepository.EliminarImagen(id);
        }

        public MediasProducto GuardarImagen(MediasProducto data)
        {
            return _mediasProductoRepository.GuardarImagen(data);
        }

        public Multimedia GuardarMultimedia(Multimedia multimedia)
        {
            return _multimediaRepository.GuardarMultimedia(multimedia);
        }

        public Multimedia GetMultimedia(Guid id)
        {
            return _multimediaRepository.FindBy(x => x.Codigo == id);
        }

        public List<Producto> GetProductosTienda(string palabra)
        {
            return _productoRepository.GetLista(x => x.Activo == true && (palabra == null || x.Nombre.Contains(palabra) || x.Descripcion.Contains(palabra)),null,new System.Linq.Expressions.Expression<Func<Producto, object>>[] { x=>x.Multimedias });
        }
    }
}
