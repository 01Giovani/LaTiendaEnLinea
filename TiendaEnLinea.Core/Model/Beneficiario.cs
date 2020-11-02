using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.Model
{
    public class Beneficiario
    {
        public Guid Codigo { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string LatitudUbicacion { get; set; }
        public string LongitudUbicacion { get; set; }
        public string IdCliente { get; set; }

        public Cliente Cliente { get; set; }
    }
}
