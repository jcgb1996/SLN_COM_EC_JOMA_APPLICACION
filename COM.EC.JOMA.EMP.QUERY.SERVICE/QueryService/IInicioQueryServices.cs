using COM.EC.JOMA.EMP.QUERY.Dtos;
using COM.EC.JOMA.EMP.QUERY.Interfaces;
using COM.EC.JOMA.EMP.QUERY.SERVICE.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.EC.JOMA.EMP.QUERY.SERVICE.QueryService
{
    public class InicioQueryServices : BaseQueryService, IInicioQueryServices
    {
        public InicioQueryServices(IServiceScopeFactory serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<LoginQueryDto> Login(string Usuario, string Clave, string Compania)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                {
                    //edocQueryContext.RealizarLogin(Usuario, Clave, Compania, "");
                    return new LoginQueryDto();
                };
            };
        }
    }
}
