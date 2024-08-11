using COM.EC.JOMA.EMP.APLICACION.Dto;
using COM.EC.JOMA.EMP.APLICACION.Interfaces;
using COM.EC.JOMA.EMP.CROSSCUTTING.Interfaces;
using COM.EC.JOMA.EMP.DOMAIN;
using COM.EC.JOMA.EMP.DOMAIN.JomaExtensions;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_EC_JOMA_APPLICACION.Controllers;

namespace SLN_COM_EC_JOMA_APPLICACION.Areas.Inicio.Controllers
{
    public class LoginController : BaseController
    {
        protected IInicioAppServices inicioAppServices;
        public LoginController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IInicioAppServices inicioAppServices) : base(logService, globalDictionary)
        {
            this.inicioAppServices = inicioAppServices;
        }

        public IActionResult Index(LoginReqAppDto Login)
        {
            inicioAppServices.LoginCompania(Login);
            return View();
        }
    }
}
