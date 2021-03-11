using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.Model
{
    public class Producto
    {
        public Guid Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionOferta { get; set; }
        public decimal Precio { get; set; }
        public decimal? PrecioOferta { get; set; }
        public bool Destacado { get; set; }
        public bool Activo { get; set; }
        public decimal MultiploVenta { get; set; }
        public string PrefijoVenta { get; set; }

        public int? IdCategoria { get; set; }

        public List<MediasProducto> Multimedias { get; set; }

        public CategoriaProducto Categoria { get; set; }
    }
}
