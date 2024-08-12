using COM.EC.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.EC.JOMA.EMP.APLICACION.Interfaces
{
    public interface ITrabajadorAppServices
    {
        Task<List<MarcacionesQueryDto>> GetMarcacionesCompania(long IdCompania);
    }
}
