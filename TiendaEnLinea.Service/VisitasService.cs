using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Repositories;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Service
{
    public class VisitasService: IVisitasService
    {
        private IVisitaRepository _visitaRepository;
        public VisitasService(IVisitaRepository visitaRepository)
        {
            _visitaRepository = visitaRepository;
        }

        public void GuardarVisita(string ip)
        {
            _visitaRepository.GuardarVisita(ip);
        }
    }
}
