using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.DTOs.Categoria
{
    public class CategoriaProductoDTO
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Nombre requerido"), MaxLength(150, ErrorMessage = "Maximo de caracteres alcanzado")]
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
