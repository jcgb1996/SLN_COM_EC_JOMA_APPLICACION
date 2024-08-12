using COM.EC.JOMA.EMP.DOMAIN.Constants;
using COM.EC.JOMA.EMP.DOMAIN.Tools;
using COM.EC.JOMA.EMP.QUERY.Dtos;
using COM.EC.JOMA.EMP.QUERY.Parameters;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace COM.EC.JOMA.EMP.QUERY.SERVICE.Model
{
    public partial class JomaQueryContext : JomaQueryContextEF
    {
        internal async Task<List<MenuQueryDto>> QRY_OpcionesManuPorIdUsuario(long IdUsuario, byte Sitio)
        {



            #region Descomentar
            var SP_NAME = "[QRY_Login]";
            //List<LoginQueryDto>? Result = new();
            //switch (QueryParameters.TipoORM)
            //{
            //    case JOMATipoORM.EntityFramework:
            //        Result = LoginQueryDto?.FromSqlRaw($"[{SP_NAME}] @p0,@p1,@p2,@p3",
            //            JOMAConversions.NothingToDBNULL(Usuario), JOMAConversions.NothingToDBNULL(ClaveEncriptada),
            //            JOMAConversions.NothingToDBNULL(Compania), JOMAConversions.NothingToDBNULL(IPLogin)).ToList();
            //
            //        break;
            //    case JOMATipoORM.Dapper:
            //        using (var connection = jomaQueryContextDP.CreateConnection())
            //        {
            //            var parameters = new DynamicParameters();
            //            parameters.Add("@LoginUsuario", JOMAConversions.NothingToDBNULL(Usuario), DbType.String);
            //            parameters.Add("@ClaveUsuario", JOMAConversions.NothingToDBNULL(ClaveEncriptada), DbType.String);
            //            parameters.Add("@RucCiaNube", JOMAConversions.NothingToDBNULL(Compania), DbType.String);
            //            parameters.Add("@IpLogin", JOMAConversions.NothingToDBNULL(IPLogin), DbType.String);
            //            Result = (await connection.QueryAsync<LoginQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).ToList();
            //        }
            //        break;
            //
            //
            //}
            #endregion
            var menu = new List<MenuQueryDto>
                {
                    new MenuQueryDto
                    {
                        IdUario = 1,
                        Title = "Trabajador",
                        Icon = "fas fa-tachometer-alt",
                        Action = "Index",
                        Controller = "Trabajador",
                        Area = "Trabajador",
                        Children = new List<MenuQueryDto>
                        {
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Marcaciones",
                                Icon = "fas fa-chart-bar",
                                Action = "Index",
                                Controller = "Marcaciones",
                                Area = "Trabajador",
                            },
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Registro",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "Registrar",
                                Area = "Trabajador"
                            }
                        }
                    },
                    new MenuQueryDto
                    {
                        IdUario = 1,
                        Title = "Configuraciones",
                        Icon = "fas fa-cogs",
                        Action = "Index",
                        Controller = "Settings",
                        Area = "Admin",
                        Children = new List<MenuQueryDto>
                        {
                            new MenuQueryDto
                            {
                        IdUario = 1,
                                Title = "Preferencias",
                                Icon = "fas fa-sliders-h",
                                Action = "Preferences",
                                Controller = "Settings",
                                Area = "Admin"
                            },
                            new MenuQueryDto
                            {
                        IdUario = 1,
                                Title = "Seguridad",
                                Icon = "fas fa-shield-alt",
                                Action = "Security",
                                Controller = "Settings",
                                Area = "Admin",
                                Children = new List<MenuQueryDto>
                                {
                                    new MenuQueryDto
                                    {
                                        Title = "Roles",
                                        Icon = "fas fa-user-shield",
                                        Action = "Roles",
                                        Controller = "Security",
                                        Area = "Admin"
                                    },
                                    new MenuQueryDto
                                    {
                                        Title = "Permisos",
                                        Icon = "fas fa-key",
                                        Action = "Permissions",
                                        Controller = "Security",
                                        Area = "Admin"
                                    }
                                }
                            }
                        }
                    }
                };

            var tarea = Task.Run(() =>
            {

                menu = menu.Where(x => x.IdUario == IdUsuario).Select(x => x).ToList();
            });

            await tarea;

            return menu != null ? menu : new List<MenuQueryDto>();
        }
    }
}
