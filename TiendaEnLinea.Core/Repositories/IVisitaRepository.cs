using Bitworks.Abstract.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.DTOs.Dashboard;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.Repositories
{
    public interface IVisitaRepository:IGenericRepository<Visita>
    {
        void GuardarVisita(string ip);
        List<ConteoActividadDTO> ActividadClientes();
    }
}
