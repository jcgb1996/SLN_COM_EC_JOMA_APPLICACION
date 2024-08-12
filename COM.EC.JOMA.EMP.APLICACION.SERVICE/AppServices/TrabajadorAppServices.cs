using COM.EC.JOMA.EMP.APLICACION.Interfaces;
using COM.EC.JOMA.EMP.CROSSCUTTING.Interfaces;
using COM.EC.JOMA.EMP.DOMAIN;
using COM.EC.JOMA.EMP.DOMAIN.JomaExtensions;
using COM.EC.JOMA.EMP.DOMAIN.Parameters;
using COM.EC.JOMA.EMP.DOMAIN.Tools;
using COM.EC.JOMA.EMP.DOMAIN.Utilities;
using COM.EC.JOMA.EMP.QUERY.Dtos;
using COM.EC.JOMA.EMP.QUERY.Interfaces;
using COM.EC.JOMA.EMP.DOMAIN.Extensions;

namespace COM.EC.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class TrabajadorAppServices : BaseAppServices, ITrabajadorAppServices
    {
        protected ITrabajadorQueryService trabajadorQueryService;
        public TrabajadorAppServices(ILogCrossCuttingService? logService, GlobalDictionaryDto globalDictionary, ITrabajadorQueryService trabajadorQueryService) : base(logService, globalDictionary)
        {
            this.trabajadorQueryService = trabajadorQueryService;
        }

        public async Task<List<MarcacionesQueryDto>> GetMarcacionesCompania(long IdCompania)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "CONSULTAR MENU POR ID USUARIO";
                var LstMarcaciones = await trabajadorQueryService.GetMarcacionesCompania(IdCompania);
                return LstMarcaciones;
            }
            catch (JOMAUException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                var CodigoSeguimiento = logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"{seccion}: {JOMAUtilities.ExceptionToString(ex)}");
                var Mensaje = globalDictionary.GenerarMensajeErrorGenerico(CodigoSeguimiento);
                throw new Exception(Mensaje);
            }

        }
    }
}
