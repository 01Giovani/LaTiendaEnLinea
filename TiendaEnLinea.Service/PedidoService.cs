using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Repositories;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Service
{
    public class PedidoService : IPedidoService
    {
        private IPedidoRepository _pedidoRepository;
        public PedidoService(
            IPedidoRepository pedidoRepository
            ) {
            _pedidoRepository = pedidoRepository;
        }
        public List<Pedido> GetPedidosPendientes()
        {
            return _pedidoRepository.GetLista(x => x.Despachado == false).OrderBy(x=>x.FechaIngreso).ToList();
        }
    }
}
