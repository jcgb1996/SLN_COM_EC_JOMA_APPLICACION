using COM.EC.JOMA.EMP.CROSSCUTTING.Interfaces;
using COM.EC.JOMA.EMP.CROSSCUTTING.SERVICE.CrossCuttingServices;
using COM.EC.JOMA.EMP.DOMAIN.Constants;
using COM.EC.JOMA.EMP.DOMAIN.Parameters;
using COM.EC.JOMA.EMP.DOMAIN.Extensions;
using Serilog;
using SLN_COM_EC_JOMA_APPLICACION.Extensions;
using COM.EC.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.EC.JOMA.EMP.QUERY.Interfaces;
using COM.EC.JOMA.EMP.QUERY.SERVICE.QueryService;
using COM.EC.JOMA.EMP.APLICACION.SERVICE.AppServices;
using COM.EC.JOMA.EMP.APLICACION.Interfaces;
using COM.EC.JOMA.EMP.DOMAIN.Utilities;
using COM.EC.JOMA.EMP.DOMAIN;
using SLN_COM_EC_JOMA_APPLICACION.Settings;
using Microsoft.VisualStudio.Web.CodeGeneration;
using COM.EC.JOMA.EMP.DOMAIN.Tools;

// Configurar Serilog antes de crear el host builder
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console() // Opcional: elimina o ajusta esta línea si no deseas salida en la consola
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    
    // Establecer parámetros de la aplicación
    DomainParameters.APP_COMPONENTE_JOMA = JOMAComponente.JomaPortalWeb;
    DomainParameters.APP_NOMBRE = $"{DomainParameters.APP_COMPONENTE_JOMA.GetNombre()} v{AppConstants.Version}";


    #region LOAD SETTINGS
    Settings settings = new Settings();
    LoadSettings(ref settings);
    #endregion

   //var a1 = JOMACrypto.CifrarClave("edocdevreg6.cynm49sjbwty.us-east-1.rds.amazonaws.com", DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
   //var a2 = JOMACrypto.CifrarClave("GSEDOC_GT", DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
   //var a3 = JOMACrypto.CifrarClave("usrkmariscal", DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
   //var a4 = JOMACrypto.CifrarClave("raD63n8NHQ1Y=zemxPHf", DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);

    // Desactivar los proveedores de log predeterminados y usar Serilog
    builder.Logging.ClearProviders();
    builder.Host.UseSerilog(); // No volvemos a llamar a CreateLogger() aquí



    #region INJECT DATABASE
    builder.Services.AddDatabase(settings?.GSEDOC_BR?.DataSource, settings?.GSEDOC_BR?.InitialCatalog, settings?.GSEDOC_BR?.UserId, settings?.GSEDOC_BR?.Password, settings?.GSEDOC_BR?.Timeout, settings?.GSEDOC_BR?.TipoORM);
    #endregion


    // Agregar servicios al contenedor
    builder.Services.AddRazorPages();

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Inicio/Login/Index"; // Esta ruta debe coincidir con el controlador y la acción de inicio de sesión
        options.AccessDeniedPath = "/Inicio/Login/AccessDenied"; // Si tienes una página de acceso denegado
    });

    builder.Services.AddRazorPages()
        .AddRazorRuntimeCompilation();

    builder.Services.AddScoped<ILogCrossCuttingService, LogCrossCuttingService>();
    builder.Services.AddScoped<IInicioAppServices, InicioAppServices>();
    builder.Services.AddScoped<IInicioQueryServices, InicioQueryServices>();
    builder.Services.AddSingleton<LogCrossCuttingService>();
    builder.Services.AddScoped<GlobalDictionaryDto>();

    var app = builder.Build();

    // Configurar el pipeline de solicitudes HTTP
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();

    app.MapRazorPages();

    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    app.MapControllerRoute(
        name: "default",
        pattern: "{area=Inicio}/{controller=Login}/{action=Index}/{id?}");

    #region ESCRIBIR LOG INICIO
    app.Services.WriteLogInitApp();
    #endregion

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(JOMAUtilities.ExceptionToString(ex));
}


void LoadSettings(ref Settings settings)
{
    JOMAUtilities.SetCultureInfo(DomainConstants.EDOC_CULTUREINFO);
    string? mensaje = null;
    string jsonSettings = File.ReadAllText(JOMAUtilities.GetFileNameAppSettings());
    settings = JOMAConversions.DeserializeJsonObject<Settings>(jsonSettings, ref mensaje);
    if (settings == null) throw new Exception(mensaje);
}
