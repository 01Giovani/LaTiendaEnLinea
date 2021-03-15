using Bitworks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.DTOs.Dashboard;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Repositories;

namespace TiendaEnLinea.SQL.Repositories
{
    public class VisitaRepository : GenericRepository<Visita>, IVisitaRepository
    {
        private TiendaEnLineaContext _db;
        public VisitaRepository(BaseContext context) : base(context)
        {
            _db = context as TiendaEnLineaContext;
        }     

        void IVisitaRepository.GuardarVisita(string ip)
        {
            _db.Visita.Add(new Visita()
            {
                FechaIngreso = DateTimeOffset.Now,
                Ip = ip
            });
            _db.SaveChanges();
        }

        public List<ConteoActividadDTO> ActividadClientes()
        {
            List<ConteoActividadDTO> actividad =
            _db.Database.SqlQuery<ConteoActividadDTO>("exec [dbo].[spActividadDias]").ToList<ConteoActividadDTO>();

            return actividad ?? new List<ConteoActividadDTO>();
        }
    }
}
