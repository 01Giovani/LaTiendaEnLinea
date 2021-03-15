using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.DTOs.Dashboard;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Repositories;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Service
{
    public class DashboardService: IDashboardService
    {
        private IVisitaRepository _visitaRepository;
        private IPedidoRepository _pedidoRepository;
        public DashboardService(
            IPedidoRepository pedidoRepository,
            IVisitaRepository visitaRepository
            )
        {
            _pedidoRepository = pedidoRepository;
            _visitaRepository = visitaRepository;
        }

        public DashboardDTO GetDataDashboard()
        {
            DashboardDTO response = new DashboardDTO();
            DateTimeOffset ahora = DateTimeOffset.Now;

            decimal totalPedidosMes = _pedidoRepository.GetTotalPedidosMes();

            decimal? cantidadPedidosAño = _pedidoRepository.CountBy(x => x.FechaIngreso.Year == ahora.Year);

            decimal? visitasHoy =_visitaRepository.CountBy(x => 
            x.FechaIngreso.Month == ahora.Month && 
            x.FechaIngreso.Year == ahora.Year && 
            x.FechaIngreso.Day == ahora.Day);

            ConteoActividadDTO actividad = _visitaRepository
                .ActividadClientes().OrderBy(x => x.cantidad).FirstOrDefault();


            response.IngresosMensuales = totalPedidosMes;
            response.MenorActividadClientes = actividad;
            response.PedidosAnuales = cantidadPedidosAño != null ? (int)cantidadPedidosAño: 0;
            response.VisitasHoy = visitasHoy != null ? (int)visitasHoy : 0;

            List<Pedido> pedidos = _pedidoRepository.GetLista(x => x.FechaIngreso.Month == ahora.Month && x.FechaIngreso.Year == ahora.Year);

            response.TotalPedidosMes = pedidos.Count();

            var a = pedidos
                .Where(x=>x.IdEstado == EstadoPedido.Entregado)
                .Select(x => new { x.Codigo, x.FechaIngreso })
                .GroupBy(x => new { x.FechaIngreso.Month, x.FechaIngreso.Day });
            
            foreach (var item in a)
            {
                response.PedidosFinalizadosMes.Add(new PedidoDashboard() { 
                    Cantidad = item.Count(),
                    Fecha = item.Select(c=>c.FechaIngreso).First()
                });
            }

            var b = pedidos
                .Where(x => x.IdEstado == EstadoPedido.Abierto)
                .Select(x => new { x.Codigo, x.FechaIngreso })
                .GroupBy(x => new { x.FechaIngreso.Month, x.FechaIngreso.Day });

            foreach (var item in b)
            {
                response.PedidosNoFinalizadosMes.Add(new PedidoDashboard() { 
                    Cantidad = item.Count(),
                    Fecha = item.Select(c=>c.FechaIngreso).First()
                });
            }

            DateTimeOffset mesAnterios = DateTimeOffset.Now.AddMonths(-1);
            decimal? pedidosMesAnterior = _pedidoRepository
                .CountBy(x => x.FechaIngreso.Month == mesAnterios.Month && x.FechaIngreso.Year == mesAnterios.Year);

            response.PedidosMesAnterior = pedidosMesAnterior != null ? (int)pedidosMesAnterior : 0;

            response.IngresosAparentesMes = pedidos.Sum(x => x.Total) ?? 0;

            return response;
        }
    }
}
