using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.SQL.Mapping
{
    public class ProductoPedidoMap: EntityTypeConfiguration<ProductosPedido>
    {
        public ProductoPedidoMap()
        {
            HasKey(x => x.Codigo);

            Property(x => x.Codigo).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);


            HasRequired(x => x.Pedido).WithMany().HasForeignKey(x => x.IdPedido);
            HasRequired(x => x.Producto).WithMany().HasForeignKey(x => x.IdProducto);

        }
    }
}
