using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.SQL.Mapping
{
    public class MultimediaMap: EntityTypeConfiguration<Multimedia>
    {
        public MultimediaMap()
        {
            HasKey(x => x.Codigo);

            Property(x => x.NombreArchivo).HasMaxLength(150);
            Property(x => x.MimeType).HasMaxLength(150);

        }
    }
}
