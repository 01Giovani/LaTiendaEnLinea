using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.DTOs.Dashboard;

namespace TiendaEnLinea.Core.Services
{
    public interface IDashboardService
    {
        DashboardDTO GetDataDashboard();
    }
}
