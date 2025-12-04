using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webappacademica.Models;

namespace webappacademica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MatriculasController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Matriculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetMatriculas()
        {
            //return await _context.Matriculas.ToListAsync();
            return await _context.Matriculas
                .Include(m => m.Alumno)
                .Include(m => m.Periodo)
                .Select(m => new {
                    m.idMatricula,
                    m.idPeriodo,
                    m.idAlumno,
                    m.fecha,
                    alumno = new
                    {
                        m.Alumno!.idAlumno,
                        m.Alumno!.codigo,
                        m.Alumno!.nombre,
                        m.Alumno!.direccion,
                        m.Alumno!.telefono
                    },
                    periodo = new
                    {
                        m.Periodo!.idPeriodo,
                        m.Periodo!.fecha,
                        m.Periodo!.periodo
                    }
                })
              .ToListAsync();
        }

        // GET: api/Matriculas/buscar
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<object>>> BuscarMatricula([FromQuery] MatriculaBusquedaParametro parametros)
        {
            var consulta = _context.Matriculas
                .Include(m => m.Alumno)
                .Include(m => m.Periodo)
                .Select(m => new {
                    m.idMatricula,
                    m.idPeriodo,
                    m.idAlumno,
                    m.fecha,
                    alumno = new
                    {
                        m.Alumno!.idAlumno,
                        m.Alumno!.codigo,
                        m.Alumno!.nombre,
                        m.Alumno!.direccion,
                        m.Alumno!.telefono
                    },
                    periodo = new
                    {
                        m.Periodo!.idPeriodo,
                        m.Periodo!.fecha,
                        m.Periodo!.periodo
                    }
                }).AsQueryable();
            if (!string.IsNullOrEmpty(parametros.buscar))
            {
                consulta = consulta.Where(matricula =>
                   matricula.fecha.ToString().Contains(parametros.buscar) ||
                   matricula.alumno.nombre.Contains(parametros.buscar));
            }
            return await consulta.ToListAsync();
        }

        // GET: api/Matriculas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matricula>> GetMatricula(int id)
        {
            //var matricula = await _context.Matriculas.FindAsync(id);
            var matricula = await _context.Matriculas
                .Include(m => m.Alumno)
                .Include(m => m.Periodo)
                .FirstOrDefaultAsync(m => m.idMatricula == id);

            if (matricula == null)
            {
                return NotFound();
            }
            return matricula;
        }

        // PUT: api/Matriculas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatricula(int id, Matricula matricula)
        {
            if (id != matricula.idMatricula)
            {
                return BadRequest();
            }

            _context.Entry(matricula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatriculaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetMatricula", new { id = matricula.idMatricula }, matricula);
        }

        // POST: api/Matriculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Matricula>> PostMatricula(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatricula", new { id = matricula.idMatricula }, matricula);
        }

        // DELETE: api/Matriculas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatricula(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matriculas.Any(e => e.idMatricula == id);
        }
    }
}