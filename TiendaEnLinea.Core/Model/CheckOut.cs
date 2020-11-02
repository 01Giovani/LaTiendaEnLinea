using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.Model
{
    public class CheckOut
    {
        public int Codigo { get; set; }
        public Guid IdPedido { get; set; }
        public int IdDetallePedido { get; set; }
        public bool Preparado { get; set; }
        public string Comentario { get; set; }

        public Pedido Pedido { get; set; }
        public ProductosPedido Detalle { get; set; }
    }
}
