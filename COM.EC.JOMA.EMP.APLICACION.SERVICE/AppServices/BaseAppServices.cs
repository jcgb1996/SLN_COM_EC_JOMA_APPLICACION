using COM.EC.JOMA.EMP.CROSSCUTTING.Interfaces;
using COM.EC.JOMA.EMP.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.EC.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class BaseAppServices
    {
        protected ILogCrossCuttingService? logService; 
        protected GlobalDictionaryDto globalDictionary;

        public BaseAppServices(ILogCrossCuttingService? logService, GlobalDictionaryDto globalDictionary)
        {
            this.logService = logService;
            this.globalDictionary = globalDictionary;
        }
    }
}
