using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.DTOs.Pedido
{
    public class ListaPedidosDTO
    {
        public Guid Codigo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IdCliente { get; set; }
        public Guid? IdBeneficiario { get; set; }
        public decimal? Total { get; set; }
        public bool Despachado { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaEntregado { get; set; }

        public Cliente Cliente { get; set; }
        public Beneficiario Beneficiario { get; set; }
        public List<ProductosPedido> ProductosPedidos { get; set; }
    }
}
