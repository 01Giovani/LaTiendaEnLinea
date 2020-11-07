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

        public ProductosPedido Agregar(ProductosPedido productosPedido)
        {
            _db.ProductosPedido.Add(productosPedido);
            _db.SaveChanges();
            return productosPedido;
        }

        public void Eliminar(int Codigo)
        {
            ProductosPedido productosPedido = _db.ProductosPedido.FirstOrDefault(x => x.Codigo == Codigo);
            if(productosPedido != null)
            {
                _db.ProductosPedido.Remove(productosPedido);
                _db.SaveChanges();

            }
            
        }

        public ProductosPedido Modificar(ProductosPedido detalle)
        {
            ProductosPedido remoto = _db.ProductosPedido.FirstOrDefault(x => x.Codigo == detalle.Codigo);

            remoto.Cantidad = detalle.Cantidad;
            remoto.SubTotal = detalle.SubTotal;
            remoto.Modificado = true;

            
            _db.SaveChanges();

            return remoto;
        }
    }
}
