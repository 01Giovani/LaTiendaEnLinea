using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.DTOs.Pedido
{
    public class CheckoutListaDTO
    {
        public List<CheckOut> Detalles { get; set; }
        public Core.Model.Pedido Pedido { get; set; }
    }
}
