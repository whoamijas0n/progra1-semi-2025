using fiexpress.Data;
using fiexpress.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FichajesController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<FichajesController> _logger;

        public FichajesController(MyDbContext context, ILogger<FichajesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/fichajes/rfid - Para el ESP32
        [HttpPost("rfid")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarFichajeRFID([FromBody] FichajeRFIDRequest request)
        {
            try
            {
                // Buscar tarjeta RFID
                var rfid = await _context.Rfids
                    .Include(r => r.Empleado)
                    .FirstOrDefaultAsync(r => r.codigo_rfid == request.CodigoRFID && r.activo);

                if (rfid == null)
                {
                    return BadRequest(new { mensaje = "Tarjeta no válida" });
                }

                var empleado = rfid.Empleado;
                if (!empleado.activo)
                {
                    return BadRequest(new { mensaje = "Empleado inactivo" });
                }

                var hoy = DateOnly.FromDateTime(DateTime.Now);
                var ahora = TimeOnly.FromDateTime(DateTime.Now);

                // Determinar tipo de fichaje
                var ultimoFichaje = await _context.Fichajes
                    .Where(f => f.idEmpleadoFichaje == empleado.idEmpleado && f.fecha == hoy)
                    .OrderByDescending(f => f.hora)
                    .FirstOrDefaultAsync();

                string tipoFichaje = "Entrada";
                if (ultimoFichaje != null)
                {
                    tipoFichaje = ultimoFichaje.tipo == "Entrada" ? "Salida" : "Entrada";
                }

                // Crear fichaje
                var fichaje = new Fichaje
                {
                    idEmpleadoFichaje = empleado.idEmpleado,
                    fecha = hoy,
                    hora = ahora,
                    tipo = tipoFichaje,
                    ip = request.IP,
                    observacion = "Registro por RFID"
                };

                _context.Fichajes.Add(fichaje);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    mensaje = $"Fichaje de {tipoFichaje} registrado",
                    empleado = empleado.nombre,
                    hora = ahora.ToString("HH:mm"),
                    tipo = tipoFichaje
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar fichaje RFID");
                return StatusCode(500, new { mensaje = "Error interno" });
            }
        }

        // GET: api/fichajes/empleado/5?fecha=2024-01-01
        [HttpGet("empleado/{idEmpleado}")]
        [Authorize]
        public async Task<IActionResult> GetFichajesPorEmpleado(int idEmpleado, [FromQuery] string fecha = null)
        {
            try
            {
                var query = _context.Fichajes
                    .Where(f => f.idEmpleadoFichaje == idEmpleado);

                if (!string.IsNullOrEmpty(fecha) && DateOnly.TryParse(fecha, out var fechaFiltro))
                {
                    query = query.Where(f => f.fecha == fechaFiltro);
                }

                var fichajes = await query
                    .OrderBy(f => f.fecha)
                    .ThenBy(f => f.hora)
                    .Select(f => new
                    {
                        f.idFichaje,
                        f.fecha,
                        hora = f.hora.ToString("HH:mm"),
                        f.tipo,
                        f.observacion
                    })
                    .ToListAsync();

                return Ok(fichajes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fichajes");
                return StatusCode(500, new { mensaje = "Error al obtener fichajes" });
            }
        }

        // GET: api/fichajes/hoy
        [HttpGet("hoy")]
        [Authorize]
        public async Task<IActionResult> GetFichajesHoy()
        {
            try
            {
                var hoy = DateOnly.FromDateTime(DateTime.Now);

                var fichajes = await _context.Fichajes
                    .Include(f => f.Empleado)
                    .Where(f => f.fecha == hoy)
                    .OrderByDescending(f => f.hora)
                    .Select(f => new
                    {
                        f.idFichaje,
                        f.fecha,
                        hora = f.hora.ToString("HH:mm"),
                        f.tipo,
                        empleado = f.Empleado.nombre
                    })
                    .ToListAsync();

                return Ok(fichajes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fichajes de hoy");
                return StatusCode(500, new { mensaje = "Error al obtener fichajes" });
            }
        }
    }

    public class FichajeRFIDRequest
    {
        public string CodigoRFID { get; set; }
        public string IP { get; set; }
    }
}