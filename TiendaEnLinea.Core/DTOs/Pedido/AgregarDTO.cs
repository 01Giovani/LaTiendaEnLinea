using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.DTOs.Pedido
{
    public class AgregarDTO
    {
        public Guid IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int? codigoDetalle { get; set; }
    }
}
