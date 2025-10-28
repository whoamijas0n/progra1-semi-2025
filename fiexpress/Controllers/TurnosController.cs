using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fiexpress.Data;
using fiexpress.Models;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Create([FromBody] Turno turno)
        {
            try
            {
                turno.activo = true;
                _context.Turnos.Add(turno);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = turno.idTurno }, new
                {
                    mensaje = "Turno creado exitosamente",
                    turno.idTurno
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear turno");
                return StatusCode(500, new { mensaje = "Error al crear turno" });
            }
        }

        // PUT: api/turnos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Turno turnoActualizado)
        {
            try
            {
                var turno = await _context.Turnos.FindAsync(id);

                if (turno == null)
                {
                    return NotFound(new { mensaje = $"Turno con ID {id} no encontrado" });
                }

                turno.nombre = turnoActualizado.nombre;
                turno.hora_entrada = turnoActualizado.hora_entrada;
                turno.hora_salida = turnoActualizado.hora_salida;
                turno.tolerancia_minutos = turnoActualizado.tolerancia_minutos;
                turno.lunes = turnoActualizado.lunes;
                turno.martes = turnoActualizado.martes;
                turno.miercoles = turnoActualizado.miercoles;
                turno.jueves = turnoActualizado.jueves;
                turno.viernes = turnoActualizado.viernes;
                turno.sabado = turnoActualizado.sabado;
                turno.domingo = turnoActualizado.domingo;

                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Turno actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar turno {Id}", id);
                return StatusCode(500, new { mensaje = "Error al actualizar turno" });
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

                turno.activo = false;
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Turno desactivado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar turno {Id}", id);
                return StatusCode(500, new { mensaje = "Error al desactivar turno" });
            }
        }
    }
} 