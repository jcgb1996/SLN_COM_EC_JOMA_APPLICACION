using COM.EC.JOMA.EMP.APLICACION.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.EC.JOMA.EMP.APLICACION.Interfaces
{
    public interface IInicioAppServices
    {
        bool LoginCompania(LoginReqAppDto login);
    }
}
