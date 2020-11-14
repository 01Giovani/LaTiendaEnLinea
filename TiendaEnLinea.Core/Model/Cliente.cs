using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.Model
{
    public class Cliente
    {
        public string Codigo { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string DUI { get; set; }

        public string Direccion { get; set; }

        public List<Beneficiario> Beneficiarios { get; set; }
    }
}
