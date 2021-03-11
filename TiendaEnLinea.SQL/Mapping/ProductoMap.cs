using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.SQL.Mapping
{
    public class ProductoMap: EntityTypeConfiguration<Producto>
    {
        public ProductoMap()
        {
            HasKey(x => x.Codigo);

            Property(x => x.Nombre).HasMaxLength(500);
            Property(x => x.Descripcion).HasMaxLength(500);
            Property(x => x.PrefijoVenta).HasMaxLength(50);

            HasMany(x => x.Multimedias).WithRequired(x => x.Producto).HasForeignKey(x => x.IdProducto);

            HasOptional(x => x.Categoria).WithMany().HasForeignKey(x => x.IdCategoria);
        }
    }
}
