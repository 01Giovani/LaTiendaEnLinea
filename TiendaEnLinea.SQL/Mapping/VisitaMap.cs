using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.SQL.Mapping
{
    public class VisitaMap:EntityTypeConfiguration<Visita>
    {
        public VisitaMap()
        {
            HasKey(x => x.Codigo);

            Property(x => x.Ip).HasMaxLength(50);
        }
    }
}
