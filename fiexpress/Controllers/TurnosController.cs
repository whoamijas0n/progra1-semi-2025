using fiexpress.Data;
using fiexpress.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TurnosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<TurnosController> _logger;

        public TurnosController(MyDbContext context, ILogger<TurnosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/turnos
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool incluirInactivos = false)
        {
            try
            {
                var query = _context.Turnos.AsQueryable();

                if (!incluirInactivos)
                {
                    query = query.Where(t => t.activo);
                }

                var turnos = await query
                    .OrderBy(t => t.hora_entrada)
                    .ToListAsync();

                return Ok(turnos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener turnos");
                return StatusCode(500, new { mensaje = "Error al obtener turnos" });
            }
        }

        // GET: api/turnos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var turno = await _context.Turnos.FindAsync(id);

                if (turno == null)
                {
                    return NotFound(new { mensaje = $"Turno con ID {id} no encontrado" });
                }

                return Ok(turno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener turno {Id}", id);
                return StatusCode(500, new { mensaje = "Error al obtener turno" });
            }
        }

        // POST: api/turnos
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TurnoCreateDto dto)
        {
            try
            {
                // ✅ VALIDACIÓN DE MODELO
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                    _logger.LogWarning($"Validación fallida: {string.Join(", ", errors)}");
                    return BadRequest(new { mensaje = "Datos inválidos", errores = errors });
                }

                // ✅ VALIDAR QUE NO EXISTA UN TURNO CON EL MISMO NOMBRE
                if (await _context.Turnos.AnyAsync(t => t.nombre == dto.nombre))
                {
                    return BadRequest(new { mensaje = "Ya existe un turno con este nombre" });
                }

                // ✅ CONVERTIR STRING A TimeOnly
                if (!TimeOnly.TryParse(dto.hora_entrada, out TimeOnly horaEntrada))
                {
                    return BadRequest(new { mensaje = "Formato de hora de entrada inválido" });
                }

                if (!TimeOnly.TryParse(dto.hora_salida, out TimeOnly horaSalida))
                {
                    return BadRequest(new { mensaje = "Formato de hora de salida inválido" });
                }

                // ✅ VALIDAR QUE LA HORA DE SALIDA SEA POSTERIOR A LA DE ENTRADA
                if (horaSalida <= horaEntrada)
                {
                    return BadRequest(new { mensaje = "La hora de salida debe ser posterior a la hora de entrada" });
                }

                // ✅ VALIDAR QUE AL MENOS UN DÍA ESTÉ SELECCIONADO
                if (!dto.lunes && !dto.martes && !dto.miercoles && !dto.jueves &&
                    !dto.viernes && !dto.sabado && !dto.domingo)
                {
                    return BadRequest(new { mensaje = "Selecciona al menos un día laboral" });
                }

                var turno = new Turno
                {
                    nombre = dto.nombre.Trim(),
                    hora_entrada = horaEntrada,
                    hora_salida = horaSalida,
                    tolerancia_minutos = dto.tolerancia_minutos,
                    lunes = dto.lunes,
                    martes = dto.martes,
                    miercoles = dto.miercoles,
                    jueves = dto.jueves,
                    viernes = dto.viernes,
                    sabado = dto.sabado,
                    domingo = dto.domingo,
                    activo = true // ✅ SIEMPRE ACTIVO AL CREAR
                };

                _context.Turnos.Add(turno);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Turno creado: {turno.nombre} (ID: {turno.idTurno})");

                return CreatedAtAction(nameof(GetById), new { id = turno.idTurno }, new
                {
                    mensaje = "Turno creado exitosamente",
                    turno.idTurno
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear turno");
                return StatusCode(500, new
                {
                    mensaje = "Error interno al crear turno",
                    detalle = ex.Message
                });
            }
        }

        // PUT: api/turnos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] TurnoCreateDto dto)
        {
            try
            {
                var turno = await _context.Turnos.FindAsync(id);

                if (turno == null)
                {
                    return NotFound(new { mensaje = $"Turno con ID {id} no encontrado" });
                }

                // ✅ VALIDACIÓN DE MODELO
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { mensaje = "Datos inválidos" });
                }

                // ✅ VALIDAR nombre único (si cambió)
                if (dto.nombre != turno.nombre &&
                    await _context.Turnos.AnyAsync(t => t.nombre == dto.nombre && t.idTurno != id))
                {
                    return BadRequest(new { mensaje = "Ya existe un turno con este nombre" });
                }

                // ✅ CONVERTIR STRING A TimeOnly
                if (!TimeOnly.TryParse(dto.hora_entrada, out TimeOnly horaEntrada))
                {
                    return BadRequest(new { mensaje = "Formato de hora de entrada inválido" });
                }

                if (!TimeOnly.TryParse(dto.hora_salida, out TimeOnly horaSalida))
                {
                    return BadRequest(new { mensaje = "Formato de hora de salida inválido" });
                }

                // ✅ VALIDAR QUE LA HORA DE SALIDA SEA POSTERIOR A LA DE ENTRADA
                if (horaSalida <= horaEntrada)
                {
                    return BadRequest(new { mensaje = "La hora de salida debe ser posterior a la hora de entrada" });
                }

                // ✅ VALIDAR QUE AL MENOS UN DÍA ESTÉ SELECCIONADO
                if (!dto.lunes && !dto.martes && !dto.miercoles && !dto.jueves &&
                    !dto.viernes && !dto.sabado && !dto.domingo)
                {
                    return BadRequest(new { mensaje = "Selecciona al menos un día laboral" });
                }

                // ✅ ACTUALIZAR TURNO
                turno.nombre = dto.nombre.Trim();
                turno.hora_entrada = horaEntrada;
                turno.hora_salida = horaSalida;
                turno.tolerancia_minutos = dto.tolerancia_minutos;
                turno.lunes = dto.lunes;
                turno.martes = dto.martes;
                turno.miercoles = dto.miercoles;
                turno.jueves = dto.jueves;
                turno.viernes = dto.viernes;
                turno.sabado = dto.sabado;
                turno.domingo = dto.domingo;
                turno.activo = dto.activo;

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Turno actualizado: {turno.nombre} (ID: {turno.idTurno})");

                return Ok(new { mensaje = "Turno actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar turno {Id}", id);
                return StatusCode(500, new
                {
                    mensaje = "Error interno al actualizar turno",
                    detalle = ex.Message
                });
            }
        }

        // DELETE: api/turnos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var turno = await _context.Turnos.FindAsync(id);

                if (turno == null)
                {
                    return NotFound(new { mensaje = $"Turno con ID {id} no encontrado" });
                }

                // ✅ VERIFICAR SI EL TURNO ESTÁ ASIGNADO A ALGÚN HORARIO ACTIVO
                var tieneHorariosActivos = await _context.Horarios
                    .AnyAsync(h => h.idTurno == id && h.activo);

                if (tieneHorariosActivos)
                {
                    return BadRequest(new
                    {
                        mensaje = "No se puede desactivar un turno que está asignado a horarios activos"
                    });
                }

                // ✅ DESACTIVAR EN LUGAR DE ELIMINAR
                turno.activo = false;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Turno desactivado: {turno.nombre} (ID: {turno.idTurno})");

                return Ok(new { mensaje = "Turno desactivado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar turno {Id}", id);
                return StatusCode(500, new { mensaje = "Error al desactivar turno" });
            }
        }

        // ✅ NUEVO ENDPOINT: Activar turno
        [HttpPut("{id}/activar")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activar(int id)
        {
            try
            {
                var turno = await _context.Turnos.FindAsync(id);

                if (turno == null)
                {
                    return NotFound(new { mensaje = $"Turno con ID {id} no encontrado" });
                }

                if (turno.activo)
                {
                    return BadRequest(new { mensaje = "El turno ya está activo" });
                }

                turno.activo = true;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Turno activado: {turno.nombre} (ID: {turno.idTurno})");

                return Ok(new { mensaje = "Turno activado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al activar turno {Id}", id);
                return StatusCode(500, new { mensaje = "Error al activar turno" });
            }
        }
    }

    // ✅ DTOs PARA TURNOS
    public class TurnoCreateDto
    {
        public string nombre { get; set; }
        public string hora_entrada { get; set; }
        public string hora_salida { get; set; }
        public int tolerancia_minutos { get; set; }
        public bool lunes { get; set; }
        public bool martes { get; set; }
        public bool miercoles { get; set; }
        public bool jueves { get; set; }
        public bool viernes { get; set; }
        public bool sabado { get; set; }
        public bool domingo { get; set; }
        public bool activo { get; set; }
    }
}