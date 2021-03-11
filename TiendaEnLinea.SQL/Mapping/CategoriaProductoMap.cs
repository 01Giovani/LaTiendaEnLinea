using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.SQL.Mapping
{
    public class CategoriaProductoMap: EntityTypeConfiguration<CategoriaProducto>
    {
        public CategoriaProductoMap()
        {
            this.HasKey(x => x.Codigo);

            this.Property(x => x.Nombre).HasMaxLength(150);

            
        }
    }
}
