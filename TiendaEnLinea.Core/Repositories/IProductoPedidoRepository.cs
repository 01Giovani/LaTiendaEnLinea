using Bitworks.Abstract.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.Repositories
{
    public interface IProductoPedidoRepository: IGenericRepository<ProductosPedido>
    {
        ProductosPedido Agregar(ProductosPedido productosPedido);
        void Eliminar(int Codigo);
        ProductosPedido Modificar(ProductosPedido detalle);
    }
}
