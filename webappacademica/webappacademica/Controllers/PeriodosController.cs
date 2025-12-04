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
    public class PeriodosController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PeriodosController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Periodos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Periodo>>> GetPeriodos()
        {
            return await _context.Periodos.ToListAsync();
        }

        // GET: api/Periodos/buscar
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Periodo>>> BuscarPeriodo([FromQuery] PeriodoBusquedaParametro parametros)
        {
            var consulta = _context.Periodos.AsQueryable();
            if (!string.IsNullOrEmpty(parametros.buscar))
            {
                consulta = consulta.Where(periodo => periodo.fecha.ToString().Contains(parametros.buscar));
            }
            if (!string.IsNullOrEmpty(parametros.buscar) && consulta.Count() <= 0)
            {
                consulta = _context.Periodos.AsQueryable();
                consulta = consulta.Where(periodo => periodo.periodo.Contains(parametros.buscar));
            }
            return await consulta.ToListAsync();
        }

        // GET: api/Periodos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Periodo>> GetPeriodo(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);

            if (periodo == null)
            {
                return NotFound();
            }

            return periodo;
        }

        // PUT: api/Periodos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeriodo(int id, Periodo periodo)
        {
            if (id != periodo.idPeriodo)
            {
                return BadRequest();
            }

            _context.Entry(periodo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetPeriodo", new { id = periodo.idPeriodo }, periodo);
        }

        // POST: api/Periodos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Periodo>> PostPeriodo(Periodo periodo)
        {
            _context.Periodos.Add(periodo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeriodo", new { id = periodo.idPeriodo }, periodo);
        }

        // DELETE: api/Periodos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeriodo(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo == null)
            {
                return NotFound();
            }

            _context.Periodos.Remove(periodo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeriodoExists(int id)
        {
            return _context.Periodos.Any(e => e.idPeriodo == id);
        }
    }
}