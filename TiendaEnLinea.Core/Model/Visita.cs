using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.Model
{
    public class Visita
    {
        public int Codigo { get; set; }
        public string Ip { get; set; }
        public DateTimeOffset FechaIngreso { get; set; }
    }
}
