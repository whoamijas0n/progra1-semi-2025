using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fiexpress.Data;
using fiexpress.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HorariosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<HorariosController> _logger;

        public HorariosController(MyDbContext context, ILogger<HorariosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/horarios
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var horarios = await _context.Horarios
                    .Include(h => h.Empleado)
                    .Include(h => h.Turno)
                    .Select(h => new
                    {
                        h.idHorario,
                        h.fecha_inicio,
                        h.fecha_fin,
                        h.activo,
                        empleado = new
                        {
                            h.Empleado.idEmpleado,
                            h.Empleado.nombre,
                            h.Empleado.codigo_empleado,
                            h.Empleado.foto_url
                        },
                        turno = new
                        {
                            h.Turno.idTurno,
                            h.Turno.nombre,
                            h.Turno.hora_entrada,
                            h.Turno.hora_salida,
                            h.Turno.tolerancia_minutos,
                            h.Turno.lunes,
                            h.Turno.martes,
                            h.Turno.miercoles,
                            h.Turno.jueves,
                            h.Turno.viernes,
                            h.Turno.sabado,
                            h.Turno.domingo
                        }
                    })
                    .OrderByDescending(h => h.fecha_inicio)
                    .ToListAsync();

                return Ok(horarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener horarios");
                return StatusCode(500, new { mensaje = "Error al obtener horarios" });
            }
        }

        // GET: api/horarios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var horario = await _context.Horarios
                    .Include(h => h.Empleado)
                    .Include(h => h.Turno)
                    .FirstOrDefaultAsync(h => h.idHorario == id);

                if (horario == null)
                {
                    return NotFound(new { mensaje = $"Horario con ID {id} no encontrado" });
                }

                return Ok(new
                {
                    horario.idHorario,
                    horario.fecha_inicio,
                    horario.fecha_fin,
                    horario.activo,
                    idHorario_De_Empleado = horario.idHorario_De_Empleado,
                    idTurno = horario.idTurno
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener horario {Id}", id);
                return StatusCode(500, new { mensaje = "Error al obtener horario" });
            }
        }

        // POST: api/horarios
        [HttpPost]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Create([FromBody] HorarioCreateDto dto)
        {
            try
            {
                // Validar que el empleado exista
                var empleado = await _context.Empleados.FindAsync(dto.IdEmpleado);
                if (empleado == null || !empleado.activo)
                {
                    return BadRequest(new { mensaje = "El empleado no existe o está inactivo" });
                }

                // Validar que el turno exista
                var turno = await _context.Turnos.FindAsync(dto.IdTurno);
                if (turno == null || !turno.activo)
                {
                    return BadRequest(new { mensaje = "El turno no existe o está inactivo" });
                }

                // Desactivar horarios anteriores si están activos
                var horariosActivos = await _context.Horarios
                    .Where(h => h.idHorario_De_Empleado == dto.IdEmpleado && h.activo)
                    .ToListAsync();

                foreach (var h in horariosActivos)
                {
                    h.activo = false;
                    if (dto.FechaInicio > h.fecha_inicio)
                    {
                        h.fecha_fin = dto.FechaInicio.AddDays(-1);
                    }
                }

                // Crear nuevo horario
                var horario = new Horario
                {
                    idHorario_De_Empleado = dto.IdEmpleado,
                    idTurno = dto.IdTurno,
                    fecha_inicio = dto.FechaInicio,
                    fecha_fin = dto.FechaFin,
                    activo = true
                };

                _context.Horarios.Add(horario);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    mensaje = "Horario asignado exitosamente",
                    horario.idHorario
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear horario");
                return StatusCode(500, new { mensaje = "Error al crear horario" });
            }
        }

        // PUT: api/horarios/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Update(int id, [FromBody] HorarioUpdateDto dto)
        {
            try
            {
                var horario = await _context.Horarios.FindAsync(id);

                if (horario == null)
                {
                    return NotFound(new { mensaje = $"Horario con ID {id} no encontrado" });
                }

                // Validar que el empleado exista
                var empleado = await _context.Empleados.FindAsync(dto.IdEmpleado);
                if (empleado == null || !empleado.activo)
                {
                    return BadRequest(new { mensaje = "El empleado no existe o está inactivo" });
                }

                // Validar que el turno exista
                var turno = await _context.Turnos.FindAsync(dto.IdTurno);
                if (turno == null || !turno.activo)
                {
                    return BadRequest(new { mensaje = "El turno no existe o está inactivo" });
                }

                // Actualizar horario
                horario.idHorario_De_Empleado = dto.IdEmpleado;
                horario.idTurno = dto.IdTurno;
                horario.fecha_inicio = dto.FechaInicio;
                horario.fecha_fin = dto.FechaFin;
                horario.activo = dto.Activo;

                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Horario actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar horario {Id}", id);
                return StatusCode(500, new { mensaje = "Error al actualizar horario" });
            }
        }

        // DELETE: api/horarios/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var horario = await _context.Horarios
                    .Include(h => h.Empleado)
                    .FirstOrDefaultAsync(h => h.idHorario == id);

                if (horario == null)
                {
                    return NotFound(new { mensaje = $"Horario con ID {id} no encontrado" });
                }

                _context.Horarios.Remove(horario);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Horario eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar horario {Id}", id);
                return StatusCode(500, new { mensaje = "Error al eliminar horario" });
            }
        }

        // GET: api/horarios/empleado/{idEmpleado}
        [HttpGet("empleado/{idEmpleado}")]
        public async Task<IActionResult> GetByEmpleado(int idEmpleado)
        {
            try
            {
                var horarios = await _context.Horarios
                    .Include(h => h.Turno)
                    .Where(h => h.idHorario_De_Empleado == idEmpleado)
                    .OrderByDescending(h => h.fecha_inicio)
                    .Select(h => new
                    {
                        h.idHorario,
                        h.fecha_inicio,
                        h.fecha_fin,
                        h.activo,
                        turno = new
                        {
                            h.Turno.idTurno,
                            h.Turno.nombre,
                            h.Turno.hora_entrada,
                            h.Turno.hora_salida,
                            h.Turno.lunes,
                            h.Turno.martes,
                            h.Turno.miercoles,
                            h.Turno.jueves,
                            h.Turno.viernes,
                            h.Turno.sabado,
                            h.Turno.domingo
                        }
                    })
                    .ToListAsync();

                return Ok(horarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener horarios del empleado {Id}", idEmpleado);
                return StatusCode(500, new { mensaje = "Error al obtener horarios" });
            }
        }

        // GET: api/horarios/activo/empleado/{idEmpleado}
        [HttpGet("activo/empleado/{idEmpleado}")]
        public async Task<IActionResult> GetHorarioActivo(int idEmpleado)
        {
            try
            {
                var hoy = DateOnly.FromDateTime(DateTime.Now);

                var horario = await _context.Horarios
                    .Include(h => h.Turno)
                    .Where(h => h.idHorario_De_Empleado == idEmpleado &&
                               h.activo &&
                               h.fecha_inicio <= hoy &&
                               (h.fecha_fin == null || h.fecha_fin >= hoy))
                    .FirstOrDefaultAsync();

                if (horario == null)
                {
                    return NotFound(new { mensaje = "El empleado no tiene un horario activo" });
                }

                return Ok(new
                {
                    horario.idHorario,
                    horario.fecha_inicio,
                    horario.fecha_fin,
                    turno = new
                    {
                        horario.Turno.idTurno,
                        horario.Turno.nombre,
                        horario.Turno.hora_entrada,
                        horario.Turno.hora_salida,
                        horario.Turno.tolerancia_minutos,
                        horario.Turno.lunes,
                        horario.Turno.martes,
                        horario.Turno.miercoles,
                        horario.Turno.jueves,
                        horario.Turno.viernes,
                        horario.Turno.sabado,
                        horario.Turno.domingo
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener horario activo del empleado {Id}", idEmpleado);
                return StatusCode(500, new { mensaje = "Error al obtener horario" });
            }
        }
    }

    // DTOs para Horarios
    public class HorarioCreateDto
    {
        public int IdEmpleado { get; set; }
        public int IdTurno { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
    }

    public class HorarioUpdateDto
    {
        public int IdEmpleado { get; set; }
        public int IdTurno { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public bool Activo { get; set; }
    }
}