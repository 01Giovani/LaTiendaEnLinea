using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.SQL.Mapping
{
    public class BeneficiarioMap:EntityTypeConfiguration<Beneficiario>
    {
        public BeneficiarioMap()
        {
            HasKey(x => x.Codigo);

            Property(x => x.NombreCompleto).HasMaxLength(500);
            Property(x => x.Telefono).HasMaxLength(25).IsOptional();
            Property(x => x.Direccion).HasMaxLength(500).IsOptional();
            Property(x => x.LatitudUbicacion).HasMaxLength(20).IsOptional();
            Property(x => x.LongitudUbicacion).HasMaxLength(20).IsOptional();
            Property(x => x.IdCliente).HasMaxLength(150);

            HasRequired(x => x.Cliente).WithMany().HasForeignKey(x => x.IdCliente);
        }
    }
}
