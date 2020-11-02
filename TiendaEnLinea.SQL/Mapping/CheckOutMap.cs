using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.SQL.Mapping
{
    public class CheckOutMap: EntityTypeConfiguration<CheckOut>
    {
        public CheckOutMap() {

            HasKey(x => x.Codigo);

            Property(x => x.Codigo).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Comentario).HasMaxLength(500);

            HasRequired(x => x.Pedido).WithMany().HasForeignKey(x => x.IdPedido);
            HasRequired(x => x.Detalle).WithMany().HasForeignKey(x => x.IdDetallePedido);
        }
    }
}
