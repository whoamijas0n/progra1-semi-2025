using fiexpress.Controllers;
using fiexpress.Data;
using fiexpress.Models;
using fiexpress.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// CONFIGURACIÓN DE SERVICIOS
// ========================================

// Configurar Controllers con opciones JSON mejoradas
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Evitar ciclos de referencia en JSON
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

    // JSON formateado (mejor para desarrollo)
    options.JsonSerializerOptions.WriteIndented = true;

    // Ignorar propiedades null
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    // Serializar enums como strings
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// ✅ Configurar CORS para permitir peticiones desde frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins(
                "https://localhost:7071",
                "https://localhost:5001",
                "http://localhost:7071",
                "http://localhost:5001"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // ✅ IMPORTANTE: Permitir cookies/credenciales
    });
});

// Configurar DbContext con SQL Server
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            // Reintentar conexión en caso de fallo
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            );

            // Timeout para comandos largos
            sqlOptions.CommandTimeout(60);
        }
    );

    // Solo en desarrollo: Logs detallados de SQL
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

builder.Services.AddScoped<IEstadisticasService, EstadisticasService>();
// ✅ CORREGIDO: Agregar cache distribuido ANTES de sesiones
builder.Services.AddDistributedMemoryCache();

// ✅ Configurar sesiones para autenticación
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

// ✅ Configurar autenticación con cookies
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/api/auth/logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.Name = "FiExpressAuth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

builder.Services.AddAuthorization();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "FiExpress API",
        Version = "v1",
        Description = "API para Sistema de Fichajes de Empleados",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "FiExpress Team"
        }
    });
});

//AGREGANDO SERVICIOS
//SERVICIO PARA EMPLEADO
builder.Services.AddScoped<EmpleadosController>(); 

//Servicio para captura RFID
builder.Services.AddSingleton<IRfidCaptureService, RfidCaptureService>();
builder.Services.AddHttpContextAccessor();

//SERVICIO PARA MANDAR MENSAJES DE TELEGRAM

builder.Services.Configure<TelegramSettings>(
    builder.Configuration.GetSection("TelegramSettings"));

// 2. HttpClient para Telegram
builder.Services.AddHttpClient<ITelegramService, TelegramService>();

// 3. Registro del servicio
builder.Services.AddScoped<ITelegramService, TelegramService>();





var app = builder.Build();


// ✅ Inyectar HttpContextAccessor en el servicio RFID
using (var scope = app.Services.CreateScope())
{
    var captureService = scope.ServiceProvider.GetRequiredService<IRfidCaptureService>();
    var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
    captureService.RegisterHttpContextAccessor(httpContextAccessor);
}

// ========================================
// MIDDLEWARE PIPELINE
// ========================================

// ✅ IMPORTANTE: CORS debe ir PRIMERO
app.UseCors("AllowAll");

// Servir archivos estáticos (HTML, CSS, JS, imágenes)
app.UseDefaultFiles();
app.UseStaticFiles();



// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FiExpress API V1");
        options.RoutePrefix = "swagger";
    });
}


if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
// IMPORTANTE: Orden correcto del middleware

//app.UseHttpsRedirection();

// Sesiones (debe ir ANTES de Authentication)
app.UseSession();

// Autenticación y Autorización
app.UseAuthentication();
app.UseAuthorization();

// Middleware personalizado para logging de requests
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

    await next();

    logger.LogInformation($"Response: {context.Response.StatusCode}");
});

// Mapear controladores
app.MapControllers();

// Endpoint de salud
app.MapGet("/health", async (MyDbContext db) =>
{
    try
    {
        var canConnect = await db.Database.CanConnectAsync();
        return Results.Ok(new
        {
            status = canConnect ? "healthy" : "unhealthy",
            database = canConnect ? "connected" : "disconnected",
            timestamp = DateTime.Now
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: ex.Message,
            statusCode: 503,
            title: "Database connection failed"
        );
    }
}).WithTags("Health");

// Fallback para SPA (Single Page Application)
app.MapFallbackToFile("index.html");

// ✅ CORREGIDO: Verificar conexión a BD al iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = services.GetRequiredService<MyDbContext>();
        var canConnect = await context.Database.CanConnectAsync();

        if (canConnect)
        {
            logger.LogInformation("✅ Conexión a base de datos exitosa");

            // Información de las tablas
            var empleados = await context.Empleados.CountAsync();
            var departamentos = await context.Departamentos.CountAsync();

            logger.LogInformation($"📊 Base de datos: {empleados} empleados, {departamentos} departamentos");
        }
        else
        {
            logger.LogError("❌ No se pudo conectar a la base de datos");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ Error al verificar la conexión a la base de datos");
    }

    // ✅ CORREGIDO: logger ahora está dentro del scope
    logger.LogInformation("🚀 FiExpress API iniciada correctamente");
}



app.Run();