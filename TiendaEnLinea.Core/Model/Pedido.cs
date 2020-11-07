using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.Model
{
    public class Pedido
    {
        public Guid Codigo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IdCliente { get; set; }
        public Guid? IdBeneficiario { get; set; }
        public decimal? Total { get; set; }
        public bool Despachado { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaEntregado { get; set; }

        /// <summary>
        /// util si se quiere hacer una lista de entregas por orden de ubicacion por ejemplo
        /// </summary>
        public int? OrdenEntrega { get; set; }

        public bool Completado { get; set; }
        public DateTime? FechaCompletado { get; set; }

        public Cliente Cliente { get; set; }
        public Beneficiario Beneficiario { get; set; }
        public List<ProductosPedido> ProductosPedidos { get; set; }

    }
}
