using Bitworks.Abstract.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;

namespace TiendaEnLinea.Core.Repositories
{
    public interface IMultimediaRepository: IGenericRepository<Multimedia>
    {
        Multimedia GuardarMultimedia(Multimedia multimedia);
    }
}
