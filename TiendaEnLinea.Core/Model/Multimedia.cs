using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.Model
{
    public class Multimedia
    {
        public Guid Codigo { get; set; }
        public string NombreArchivo { get; set; }
        public string MimeType { get; set; }
        public byte[] Contenido { get; set; }
    }
}
