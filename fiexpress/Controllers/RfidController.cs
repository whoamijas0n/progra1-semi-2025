// ========================================
// ARCHIVO: Controllers/RfidController.cs
// Gestión de Tarjetas RFID
// ========================================
using fiexpress.Data;
using fiexpress.Models;
using fiexpress.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] // ⚠️ Comentar para desarrollo
    public class RfidController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<RfidController> _logger;
        private readonly IRfidCaptureService _captureService;

        public RfidController(MyDbContext context, ILogger<RfidController> logger, IRfidCaptureService captureService)
        {
            _context = context;
            _logger = logger;
            _captureService = captureService;
        }

        // POST: api/rfid/capture/start
        [HttpPost("capture/start")]
        [Authorize(Roles = "Admin,Supervisor")]
        public IActionResult StartCapture([FromQuery] int duration = 30)
        {
            var supervisorId = int.Parse(User.FindFirst("SupervisorId")?.Value
                              ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            _captureService.StartCapture(supervisorId, duration);
            return Ok(new { mensaje = "Modo de captura activado", duracion_segundos = duration });
        }

        // GET: api/rfid/capture/result
        [HttpGet("capture/result")]
        [Authorize(Roles = "Admin,Supervisor")]
        public IActionResult GetCaptureResult()
        {
            var supervisorId = int.Parse(User.FindFirst("SupervisorId")?.Value
                              ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var rfid = _captureService.GetLastCapturedRfid(supervisorId);
            return Ok(new { rfid });
        }

        // GET: api/rfid/capture/status
        [HttpGet("capture/status")]
        [Authorize(Roles = "Admin,Supervisor")]
        public IActionResult GetCaptureStatus()
        {
            var supervisorId = int.Parse(User.FindFirst("SupervisorId")?.Value
                              ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var activo = _captureService.IsCaptureActive(supervisorId);
            return Ok(new { activo });
        }

        // POST: api/rfid/capture/unknown (para ESP32)
        [HttpPost("capture/unknown")]
        [AllowAnonymous]
        public IActionResult CaptureUnknown([FromBody] CapturaRfidDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.codigoRfid))
            {
                _captureService.CaptureUnknownRfid(dto.codigoRfid);
                return Ok(new { mensaje = "Código RFID capturado para supervisores activos" });
            }
            return BadRequest(new { mensaje = "Código RFID requerido" });
        }

      

        // GET: api/rfid
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool incluirInactivos = false)
        {
            try
            {
                var query = _context.Rfids
                    .Include(r => r.Empleado)
                        .ThenInclude(e => e.Departamento)
                    .AsQueryable();

                if (!incluirInactivos)
                {
                    query = query.Where(r => r.activo);
                }

                var rfids = await query
                    .Select(r => new
                    {
                        r.idRfid,
                        r.codigo_rfid,
                        r.fecha_asignacion,
                        r.activo,
                        empleado = new
                        {
                            r.Empleado.idEmpleado,
                            r.Empleado.codigo_empleado,
                            r.Empleado.nombre,
                            r.Empleado.email,
                            r.Empleado.foto_url,
                            departamento = r.Empleado.Departamento.nombre
                        }
                    })
                    .OrderBy(r => r.empleado.nombre)
                    .ToListAsync();

                return Ok(rfids);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tarjetas RFID");
                return StatusCode(500, new { mensaje = "Error al obtener tarjetas RFID" });
            }
        }

        // GET: api/rfid/empleado/{idEmpleado}
        [HttpGet("empleado/{idEmpleado}")]
        public async Task<IActionResult> GetByEmpleado(int idEmpleado)
        {
            try
            {
                var rfids = await _context.Rfids
                    .Where(r => r.idEmpleadoAsignado == idEmpleado)
                    .OrderByDescending(r => r.fecha_asignacion)
                    .ToListAsync();

                return Ok(rfids);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tarjetas del empleado {Id}", idEmpleado);
                return StatusCode(500, new { mensaje = "Error al obtener tarjetas" });
            }
        }

        // GET: api/rfid/verificar/{codigoRfid}
        [HttpGet("verificar/{codigoRfid}")]
        [AllowAnonymous] // El ESP32 puede verificar sin autenticación
        public async Task<IActionResult> VerificarTarjeta(string codigoRfid)
        {
            try
            {
                var rfid = await _context.Rfids
                    .Include(r => r.Empleado)
                        .ThenInclude(e => e.Departamento)
                    .Include(r => r.Empleado)
                        .ThenInclude(e => e.Rol)
                    .FirstOrDefaultAsync(r => r.codigo_rfid == codigoRfid && r.activo);

                if (rfid == null)
                {
                    return NotFound(new
                    {
                        mensaje = "Tarjeta no encontrada o inactiva",
                        valida = false
                    });
                }

                if (!rfid.Empleado.activo)
                {
                    return Ok(new
                    {
                        mensaje = "Empleado inactivo",
                        valida = false
                    });
                }

                return Ok(new
                {
                    mensaje = "Tarjeta válida",
                    valida = true,
                    empleado = new
                    {
                        rfid.Empleado.idEmpleado,
                        rfid.Empleado.codigo_empleado,
                        rfid.Empleado.nombre,
                        rfid.Empleado.email,
                        departamento = rfid.Empleado.Departamento.nombre,
                        rol = rfid.Empleado.Rol.nombre
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar tarjeta {Codigo}", codigoRfid);
                return StatusCode(500, new { mensaje = "Error al verificar tarjeta" });
            }
        }

        // POST: api/rfid
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RfidCreateDto dto)
        {
            try
            {
                // Validar que no exista el código RFID
                if (await _context.Rfids.AnyAsync(r => r.codigo_rfid == dto.CodigoRfid))
                {
                    return BadRequest(new { mensaje = "El código RFID ya existe" });
                }

                // Validar que el empleado exista
                var empleado = await _context.Empleados.FindAsync(dto.IdEmpleado);
                if (empleado == null)
                {
                    return BadRequest(new { mensaje = "El empleado no existe" });
                }

                var rfid = new Rfid
                {
                    codigo_rfid = dto.CodigoRfid,
                    idEmpleadoAsignado = dto.IdEmpleado,
                    fecha_asignacion = DateOnly.FromDateTime(DateTime.Now),
                    activo = true
                };

                _context.Rfids.Add(rfid);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAll), new { id = rfid.idRfid }, new
                {
                    mensaje = "Tarjeta RFID asignada exitosamente",
                    rfid.idRfid
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear tarjeta RFID");
                return StatusCode(500, new { mensaje = "Error al crear tarjeta RFID" });
            }
        }

        // PUT: api/rfid/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RfidUpdateDto dto)
        {
            try
            {
                var rfid = await _context.Rfids.FindAsync(id);

                if (rfid == null)
                {
                    return NotFound(new { mensaje = $"Tarjeta RFID con ID {id} no encontrada" });
                }

                // Validar código único (si cambió)
                if (dto.CodigoRfid != rfid.codigo_rfid &&
                    await _context.Rfids.AnyAsync(r => r.codigo_rfid == dto.CodigoRfid && r.idRfid != id))
                {
                    return BadRequest(new { mensaje = "El código RFID ya existe" });
                }

                rfid.codigo_rfid = dto.CodigoRfid;
                rfid.idEmpleadoAsignado = dto.IdEmpleado;

                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Tarjeta RFID actualizada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar tarjeta RFID {Id}", id);
                return StatusCode(500, new { mensaje = "Error al actualizar tarjeta RFID" });
            }
        }

        // DELETE: api/rfid/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var rfid = await _context.Rfids.FindAsync(id);

                if (rfid == null)
                {
                    return NotFound(new { mensaje = $"Tarjeta RFID con ID {id} no encontrada" });
                }

                // Desactivar en lugar de eliminar
                rfid.activo = false;
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Tarjeta RFID desactivada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar tarjeta RFID {Id}", id);
                return StatusCode(500, new { mensaje = "Error al desactivar tarjeta RFID" });
            }
        }

        // PUT: api/rfid/{id}/activar
        [HttpPut("{id}/activar")]
        public async Task<IActionResult> Activar(int id)
        {
            try
            {
                var rfid = await _context.Rfids.FindAsync(id);

                if (rfid == null)
                {
                    return NotFound(new { mensaje = $"Tarjeta RFID con ID {id} no encontrada" });
                }

                rfid.activo = true;
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Tarjeta RFID activada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al activar tarjeta RFID {Id}", id);
                return StatusCode(500, new { mensaje = "Error al activar tarjeta RFID" });
            }
        }




        // GET: api/rfid/empleados-sin-tarjeta
        [HttpGet("empleados-sin-tarjeta")]
        public async Task<IActionResult> GetEmpleadosSinTarjeta()
        {
            try
            {
                // Obtener IDs de empleados que YA tienen tarjeta activa
                var empleadosConTarjeta = await _context.Rfids
                    .Where(r => r.activo)
                    .Select(r => r.idEmpleadoAsignado)
                    .ToListAsync();

                // Obtener empleados activos SIN tarjeta
                var empleados = await _context.Empleados
                    .Include(e => e.Departamento)
                    .Where(e => e.activo && !empleadosConTarjeta.Contains(e.idEmpleado))
                    .Select(e => new
                    {
                        e.idEmpleado,
                        e.codigo_empleado,
                        e.nombre,
                        e.email,
                        departamento = e.Departamento.nombre
                    })
                    .OrderBy(e => e.nombre)
                    .ToListAsync();

                return Ok(empleados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener empleados sin tarjeta");
                return StatusCode(500, new { mensaje = "Error al obtener empleados" });
            }
        }
    }

    // DTOs
    public class RfidCreateDto
    {
        [Required, MaxLength(50)]
        public string CodigoRfid { get; set; }

        [Required]
        public int IdEmpleado { get; set; }
    }

    public class RfidUpdateDto
    {
        [Required, MaxLength(50)]
        public string CodigoRfid { get; set; }

        [Required]
        public int IdEmpleado { get; set; }
    }
    public class CapturaRfidDto
    {
        public string codigoRfid { get; set; }
    }
}