using COM.EC.JOMA.EMP.QUERY.Dtos;
using COM.EC.JOMA.EMP.QUERY.Interfaces;
using COM.EC.JOMA.EMP.QUERY.SERVICE.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.EC.JOMA.EMP.QUERY.SERVICE.QueryService
{
    public class TrabajadorQueryService : BaseQueryService, ITrabajadorQueryService
    {
        public TrabajadorQueryService(IServiceScopeFactory serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<MarcacionesQueryDto>> GetMarcacionesCompania(long IdCompania)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await edocQueryContext.QRY_Marcaciones(IdCompania);
                        //return new LoginQueryDto();
                    };
                };




            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"SQL Error: {sqlEx.Message} Error Number: {sqlEx.Number}");
            }
            catch (TimeoutException timeoutEx)
            {
                throw new Exception($"Timeout Error: {timeoutEx.Message}");
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
