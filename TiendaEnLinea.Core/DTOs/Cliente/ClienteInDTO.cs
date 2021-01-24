using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.DTOs.Cliente
{
    public class ClienteInDTO
    {
        
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Nombre requerido"), MaxLength(500, ErrorMessage = "Maximo de caracteres alcanzado")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "Teléfono requerido"), MaxLength(25, ErrorMessage = "Maximo de caracteres alcanzado")]
        public string Telefono { get; set; }
        public string DUI { get; set; }

        [Required(ErrorMessage = "Dirección requerida"), MaxLength(500, ErrorMessage = "Maximo de caracteres alcanzado")]
        public string Direccion { get; set; }
    }
}
