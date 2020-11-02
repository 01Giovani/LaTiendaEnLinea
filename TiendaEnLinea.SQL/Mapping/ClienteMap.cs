using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.SQL.Mapping
{
    public class ClienteMap: EntityTypeConfiguration<Cliente>
    {
        public ClienteMap()
        {
            HasKey(x => x.Codigo);

            Property(x => x.Codigo).HasMaxLength(150);
            Property(x => x.NombreCompleto).HasMaxLength(500);
            Property(x => x.Telefono).HasMaxLength(25).IsOptional();
            Property(x => x.DUI).HasMaxLength(20).IsOptional();

            HasMany(x => x.Beneficiarios).WithRequired(x => x.Cliente).HasForeignKey(x => x.IdCliente);
        }
    }
}
