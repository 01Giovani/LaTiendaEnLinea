using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLinea.Core.DTOs.Dashboard
{
    public class DashboardDTO
    {
        public DashboardDTO()
        {
            PedidosNoFinalizadosMes = new List<PedidoDashboard>();
            PedidosFinalizadosMes = new List<PedidoDashboard>();
        }
        public decimal IngresosMensuales { get; set; }
        public int PedidosAnuales { get; set; }
        public int VisitasHoy { get; set; }
        public ConteoActividadDTO MenorActividadClientes { get; set; }

        public List<PedidoDashboard> PedidosNoFinalizadosMes { get; set; }
        public List<PedidoDashboard> PedidosFinalizadosMes { get; set; }
        public int PedidosMesAnterior { get; set; }
        public decimal IngresosAparentesMes { get; set; }
        public int TotalPedidosMes { get; set; }
    }

    public class PedidoDashboard
    {
        public DateTimeOffset Fecha { get; set; }
        public int Cantidad { get; set; }
    }
}
