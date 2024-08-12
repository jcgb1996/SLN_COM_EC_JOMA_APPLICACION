using COM.EC.JOMA.EMP.APLICACION.Interfaces;
using COM.EC.JOMA.EMP.APLICACION.SERVICE.AppServices;
using COM.EC.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.EC.JOMA.EMP.CROSSCUTTING.Interfaces;
using COM.EC.JOMA.EMP.DOMAIN;
using COM.EC.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SLN_COM_EC_JOMA_APPLICACION.Controllers;

namespace SLN_COM_EC_JOMA_APPLICACION.Areas.Trabajador.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_TRABAJADOR)]
    public class MarcacionesController : BaseController
    {
        protected ITrabajadorAppServices trabajadorAppServices;
        public MarcacionesController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, ITrabajadorAppServices trabajadorAppServices) : base(logService, globalDictionary)
        {
            this.trabajadorAppServices = trabajadorAppServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetMarcaciones()
        {
            try
            {
                var Usuario = GetUsuarioSesion();
                var MarcacionesDto = await trabajadorAppServices.GetMarcacionesCompania(Usuario.IdCompania);
                return StatusCode(StatusCodes.Status200OK, MarcacionesDto);
            }
            catch (JOMAUException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
            finally
            {
                logService.GuardarLogs();
            }
        }
    }


}
