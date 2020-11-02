using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.Model
{
    public class MediasProducto
    {
        public int Codigo { get; set; }
        public Guid IdProducto { get; set; }
        public Guid IdMultimedia { get; set; }

        public Producto Producto { get; set; }
        public Multimedia Multimedia { get; set; }

    }
}
