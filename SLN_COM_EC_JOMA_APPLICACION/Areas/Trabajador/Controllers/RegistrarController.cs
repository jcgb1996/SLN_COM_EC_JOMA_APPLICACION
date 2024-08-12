using COM.EC.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.EC.JOMA.EMP.CROSSCUTTING.Interfaces;
using COM.EC.JOMA.EMP.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_EC_JOMA_APPLICACION.Controllers;

namespace SLN_COM_EC_JOMA_APPLICACION.Areas.Trabajador.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_TRABAJADOR)]
    public class RegistrarController : BaseController
    {
        public RegistrarController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary) : base(logService, globalDictionary)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
