using COM.EC.JOMA.EMP.CROSSCUTTING.Interfaces;
using COM.EC.JOMA.EMP.DOMAIN;
using Microsoft.AspNetCore.Mvc;

namespace SLN_COM_EC_JOMA_APPLICACION.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogCrossCuttingService logService;
        protected GlobalDictionaryDto globalDictionary;
        public BaseController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary)
        {
            this.logService = logService;
            this.globalDictionary = globalDictionary;
        }
    }
}
