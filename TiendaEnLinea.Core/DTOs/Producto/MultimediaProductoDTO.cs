using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TiendaEnLinea.Core.DTOs.Producto
{
    public class MultimediaProductoDTO
    {
        public int? Codigo { get; set; }
        public Guid IdProducto { get; set; }
        public Guid IdMultimedia { get; set; }

        public HttpPostedFileBase File { get; set; }

        [Required(ErrorMessage = "Imagen requerida")]        
        [RegularExpression(@"^.*(.png|.PNG|.jpg|.JPG|.JPEG|.jpeg)$", ErrorMessage = "Formato de foto no valido")]
        public string Name { get; set; }
    }
}
