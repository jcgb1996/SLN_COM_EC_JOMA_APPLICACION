using COM.EC.JOMA.EMP.APLICACION.Dto;
using COM.EC.JOMA.EMP.APLICACION.Interfaces;
using COM.EC.JOMA.EMP.CROSSCUTTING.Interfaces;
using COM.EC.JOMA.EMP.DOMAIN;
using COM.EC.JOMA.EMP.DOMAIN.Extensions;
using COM.EC.JOMA.EMP.DOMAIN.JomaExtensions;
using COM.EC.JOMA.EMP.DOMAIN.Parameters;
using COM.EC.JOMA.EMP.DOMAIN.Utilities;
using COM.EC.JOMA.EMP.QUERY.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace COM.EC.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class InicioAppServices : BaseAppServices, IInicioAppServices
    {
        protected IInicioQueryServices LoginQueryServices;

        public InicioAppServices(ILogCrossCuttingService? logService, GlobalDictionaryDto globalDictionary, IInicioQueryServices LoginQueryServices) : base(logService, globalDictionary)
        {
            this.logService = logService;
            this.LoginQueryServices = LoginQueryServices;
        }

        public bool LoginCompania(LoginReqAppDto login)
        {
            string seccion = string.Empty;
            try
            {
                var RealizoLogin = LoginQueryServices.Login(login.Usuario, login.Clave, login.Compania);
            }
            catch (Exception ex)
            {
                var CodigoSeguimiento = logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"{seccion}: {JOMAUtilities.ExceptionToString(ex)}");
                globalDictionary.GenerarMensajeErrorGenerico(CodigoSeguimiento);
            }
            return true;
        }
    }
}
