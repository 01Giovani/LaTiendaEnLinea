using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.DTOs.Pedido
{
    public class PreracionDTO
    {
        public int IdDetalle { get; set; }
        public string Comentario { get; set; }
        public bool Preparado { get; set; }
    }
}
