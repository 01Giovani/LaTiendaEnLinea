using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.DTOs.ProductoPublico
{
    public class ProductoCatalogoDTO
    {
        public Guid Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal? PrecioOferta { get; set; }
        public bool Destacado { get; set; }        
        public decimal MultiploVenta { get; set; }
        public string PrefijoVenta { get; set; }

        public List<Guid> Fotos { get; set; }
    }
}
