using COM.EC.JOMA.EMP.DOMAIN.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.EC.JOMA.EMP.DOMAIN.Constants
{
    public enum JOMAComponente
    {
        [JomaDetComponenteAttribute("CP001", "ECJOMAEMP_PortalWeb")]
        JomaPortalWeb
    }

    public enum JOMATipoORM : byte
    {
        EntityFramework = 1,
        Dapper = 2
    }

    public enum JOMAAmbiente : byte
    {
        [Description("pro")]
        Produccion = 1,
        [Description("qa")]
        Calidad = 2,
        [Description("dev")]
        Desarrollo = 3
    }
}
