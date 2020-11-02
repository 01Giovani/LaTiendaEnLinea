using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.DTOs.Producto
{
    public class DetalleProductoDTO
    {
        public Guid? Codigo { get; set; }

        [Required(ErrorMessage = "Nombre requerido"),MaxLength(500,ErrorMessage = "Maximo de caracteres alcanzado")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Descripción requerida"), MaxLength(500, ErrorMessage = "Maximo de caracteres alcanzado")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Precio requerido")]
        public string Precio { get; set; }
        public string PrecioOferta { get; set; }
        public bool Destacado { get; set; }
        public bool Activo { get; set; }
        public string MultiploVenta { get; set; }
        public string PrefijoVenta { get; set; }

        public List<MediasProducto> Multimedias { get; set; }


        public MultimediaProductoDTO Foto { get; set; }


    }
}
