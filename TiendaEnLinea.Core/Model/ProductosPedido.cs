using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.Model
{
    public class ProductosPedido
    {
        public int Codigo { get; set; }
        public Guid IdProducto { get; set; }
        public Guid IdPedido { get; set; }
        public decimal Cantidad { get; set; }
        public decimal SubTotal { get; set; }

        /// <summary>
        /// se coloca true si el detalle fue modificado luego de ser enviado
        /// </summary>
        public bool Modificado { get; set; }

        public Pedido Pedido { get; set; }
        public Producto Producto { get; set; }
    }
}
