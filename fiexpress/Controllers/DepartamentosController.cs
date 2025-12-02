using fiexpress.Data;
using fiexpress.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartamentosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<DepartamentosController> _logger;

        public DepartamentosController(MyDbContext context, ILogger<DepartamentosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/departamentos
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool incluirInactivos = false)
        {
            try
            {
                var query = _context.Departamentos.AsQueryable();

                if (!incluirInactivos)
                {
                    query = query.Where(d => d.activo);
                }

                var departamentos = await query
                    .Select(d => new
                    {
                        d.idDepartamento,
                        d.nombre,
                        d.codigo,
                        d.activo,
                        d.fecha_creacion,
                        totalEmpleados = d.Empleados.Count(e => e.activo)
                    })
                    .OrderBy(d => d.nombre)
                    .ToListAsync();

                return Ok(departamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener departamentos");
                return StatusCode(500, new { mensaje = "Error al obtener departamentos" });
            }
        }

        // GET: api/departamentos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var departamento = await _context.Departamentos
                    .Include(d => d.Empleados.Where(e => e.activo))
                    .Include(d => d.Supervisores.Where(s => s.activo))
                    .FirstOrDefaultAsync(d => d.idDepartamento == id);

                if (departamento == null)
                {
                    return NotFound(new { mensaje = $"Departamento con ID {id} no encontrado" });
                }

                return Ok(new
                {
                    departamento.idDepartamento,
                    departamento.nombre,
                    departamento.codigo,
                    departamento.activo,
                    departamento.fecha_creacion,
                    totalEmpleados = departamento.Empleados.Count,
                    totalSupervisores = departamento.Supervisores.Count
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener departamento {Id}", id);
                return StatusCode(500, new { mensaje = "Error al obtener departamento" });
            }
        }

        // POST: api/departamentos
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] DepartamentoCreateDto dto)
        {
            try
            {
                // Validar que no exista el código
                if (await _context.Departamentos.AnyAsync(d => d.codigo == dto.Codigo))
                {
                    return BadRequest(new { mensaje = "El código de departamento ya existe" });
                }

                var departamento = new Departamento
                {
                    nombre = dto.Nombre,
                    codigo = dto.Codigo,
                    activo = true,
                    fecha_creacion = DateTime.Now
                };

                _context.Departamentos.Add(departamento);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = departamento.idDepartamento }, new
                {
                    mensaje = "Departamento creado exitosamente",
                    departamento.idDepartamento
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear departamento");
                return StatusCode(500, new { mensaje = "Error al crear departamento" });
            }
        }

        // PUT: api/departamentos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartamentoUpdateDto dto)
        {
            try
            {
                var departamento = await _context.Departamentos.FindAsync(id);

                if (departamento == null)
                {
                    return NotFound(new { mensaje = $"Departamento con ID {id} no encontrado" });
                }

                departamento.nombre = dto.Nombre;
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Departamento actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar departamento {Id}", id);
                return StatusCode(500, new { mensaje = "Error al actualizar departamento" });
            }
        }

        // PUT: api/departamentos/{id}/activar
        [HttpPut("{id}/activar")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activar(int id)
        {
            try
            {
                var departamento = await _context.Departamentos.FindAsync(id);

                if (departamento == null)
                {
                    return NotFound(new { mensaje = $"Departamento con ID {id} no encontrado" });
                }

                if (departamento.activo)
                {
                    return BadRequest(new { mensaje = "El departamento ya está activo" });
                }

                departamento.activo = true;
                await _context.SaveChangesAsync();

                // Registrar auditoría
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userId, out int usuarioId))
                {
                    var auditoria = new Auditoria
                    {
                        idAuditoriaUsuario = usuarioId,
                        accion = "ACTIVAR_DEPARTAMENTO",
                        entidad_afectada = "Departamento",
                        fecha_accion = DateTime.Now,
                        ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                        descripcion = $"Departamento activado: {departamento.nombre}"
                    };
                    _context.Auditorias.Add(auditoria);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { mensaje = "Departamento activado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al activar departamento {Id}", id);
                return StatusCode(500, new { mensaje = "Error al activar departamento" });
            }
        }


        // DELETE: api/departamentos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var departamento = await _context.Departamentos.FindAsync(id);

                if (departamento == null)
                {
                    return NotFound(new { mensaje = $"Departamento con ID {id} no encontrado" });
                }

                // Verificar si tiene empleados activos
                var tieneEmpleados = await _context.Empleados
                    .AnyAsync(e => e.idDepartamento == id && e.activo);

                if (tieneEmpleados)
                {
                    return BadRequest(new { mensaje = "No se puede desactivar un departamento con empleados activos" });
                }

                departamento.activo = false;
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Departamento desactivado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar departamento {Id}", id);
                return StatusCode(500, new { mensaje = "Error al desactivar departamento" });
            }
        }
    }

    // DTOs para Departamentos
    public class DepartamentoCreateDto
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    public class DepartamentoUpdateDto
    {
        public string Nombre { get; set; }
    }
}