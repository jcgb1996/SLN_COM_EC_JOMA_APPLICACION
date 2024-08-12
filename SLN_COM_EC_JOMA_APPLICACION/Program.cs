using System.Globalization;
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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using COM.EC.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Localization;
using SLN_COM_EC_JOMA_APPLICACION.Middleware;

// Configurar Serilog antes de crear el host builder
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Configuración de servicios de la aplicación
    DomainParameters.APP_COMPONENTE_JOMA = JOMAComponente.JomaPortalWeb;
    DomainParameters.APP_NOMBRE = $"{DomainParameters.APP_COMPONENTE_JOMA.GetNombre()} v{AppConstants.Version}";

    #region LOAD SETTINGS
    Settings settings = new Settings();
    LoadSettings(ref settings);
    #endregion

    // Desactivar los proveedores de log predeterminados y usar Serilog
    builder.Logging.ClearProviders();
    builder.Host.UseSerilog();

    #region INJECT DATABASE
    builder.Services.AddDatabase(
        settings?.GSEDOC_BR?.DataSource,
        settings?.GSEDOC_BR?.InitialCatalog,
        settings?.GSEDOC_BR?.UserId,
        settings?.GSEDOC_BR?.Password,
        settings?.GSEDOC_BR?.Timeout,
        settings?.GSEDOC_BR?.TipoORM);
    #endregion

    // Configuración de autenticación y autorización
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Inicio/Login/Index";
            options.AccessDeniedPath = "/Inicio/Login/AccessDenied";
        });

    // Configuración de la sesión
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    // Agregar servicios al contenedor
    builder.Services.AddRazorPages()
        .AddRazorRuntimeCompilation();

    builder.Services.AddScoped<ILogCrossCuttingService, LogCrossCuttingService>();
    builder.Services.AddScoped<IInicioAppServices, InicioAppServices>();
    builder.Services.AddScoped<IInicioQueryServices, InicioQueryServices>();
    builder.Services.AddScoped<ITrabajadorAppServices, TrabajadorAppServices>();
    builder.Services.AddScoped<ITrabajadorQueryService, TrabajadorQueryService>();
    builder.Services.AddSingleton<LogCrossCuttingService>();
    builder.Services.AddScoped<GlobalDictionaryDto>();

    var app = builder.Build();

    // Configurar el pipeline de solicitudes HTTP
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }
    else
    {
        app.UseDeveloperExceptionPage();
    }

    // Habilitar NoCacheMiddleware
    app.UseMiddleware<NoCacheMiddleware>();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    // Uso de autenticación y autorización
    app.UseAuthentication();
    app.UseAuthorization();

    // Uso de sesión
    app.UseSession();
    app.UseMiddleware<SessionManagementMiddleware>();

    // Configurar la localización
    var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("es-EC") };
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("es-EC"),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures
    });

    // Mapear Razor Pages
    app.MapRazorPages();

    app.MapControllerRoute(
        name: "customLogin",
        pattern: "Login", 
        defaults: new { controller = "Login", action = "Index", area = "Inicio" });

    app.MapControllerRoute(
        name: "customDashboard",
        pattern: "Dashboard",
        defaults: new { controller = "Dashboard", action = "Index", area = "Inicio" });

    app.MapControllerRoute(
        name: "customMarcaciones",
        pattern: "Marcaciones", 
        defaults: new { controller = "Marcaciones", action = "Index", area = "Trabajador" });

    app.MapControllerRoute(
       name: "customRegistrar",
       pattern: "Registrar",
       defaults: new { controller = "Registrar", action = "Index", area = "Trabajador" });

    // Rutas tradicionales
    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

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
