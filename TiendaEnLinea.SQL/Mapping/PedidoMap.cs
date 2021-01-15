using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.SQL.Mapping
{
    public class PedidoMap: EntityTypeConfiguration<Pedido>
    {
        public PedidoMap()
        {
            HasKey(x => x.Codigo);

            Property(x => x.FechaIngreso);
            Property(x => x.IdCliente).HasMaxLength(150);
            Property(x => x.Comentario).HasMaxLength(500);

            HasOptional(x => x.Cliente).WithMany().HasForeignKey(x => x.IdCliente);
            HasOptional(x => x.Beneficiario).WithMany().HasForeignKey(x => x.IdBeneficiario);

            HasMany(x => x.ProductosPedidos).WithRequired(x => x.Pedido).HasForeignKey(x => x.IdPedido);


        }
    }
}
