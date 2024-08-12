using COM.EC.JOMA.EMP.APLICACION.Dto;
using COM.EC.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.EC.JOMA.EMP.CROSSCUTTING.Interfaces;
using COM.EC.JOMA.EMP.DOMAIN;
using COM.EC.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_EC_JOMA_APPLICACION.Controllers;

namespace SLN_COM_EC_JOMA_APPLICACION.Areas.Inicio.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_INICIO)]
    public class DashboardController : BaseController
    {
        public DashboardController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary) : base(logService, globalDictionary)
        {
        }

        public IActionResult Index()
        {
            string mensajelogin = "";
            ViewData["UsuarioSession"] = JOMAConversions.DeserializeJsonObject<LoginAppResultDto>(HttpContext.Session.GetString("UsuarioLogin"), ref mensajelogin);
            return View();
        }
    }
}
