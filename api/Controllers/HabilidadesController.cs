using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabilidadesController : ControllerBase
    {
        private readonly AppDataContext _context;

        public HabilidadesController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Habilidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habilidade>>> GetHabilidade()
        {
          if (_context.Habilidade == null)
          {
              return NotFound();
          }
            return await _context.Habilidade.ToListAsync();
        }

        // GET: api/Habilidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Habilidade>> GetHabilidade(int id)
        {
          if (_context.Habilidade == null)
          {
              return NotFound();
          }
            var habilidade = await _context.Habilidade.FindAsync(id);

            if (habilidade == null)
            {
                return NotFound();
            }

            return habilidade;
        }

        // PUT: api/Habilidades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabilidade(int id, Habilidade habilidade)
        {
            if (id != habilidade.HabilidadeId)
            {
                return BadRequest();
            }

            _context.Entry(habilidade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabilidadeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Habilidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Habilidade>> PostHabilidade(Habilidade habilidade)
        {
          if (_context.Habilidade == null)
          {
              return Problem("Entity set 'AppDataContext.Habilidade'  is null.");
          }
            _context.Habilidade.Add(habilidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHabilidade", new { id = habilidade.HabilidadeId }, habilidade);
        }

        // DELETE: api/Habilidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabilidade(int id)
        {
            if (_context.Habilidade == null)
            {
                return NotFound();
            }
            var habilidade = await _context.Habilidade.FindAsync(id);
            if (habilidade == null)
            {
                return NotFound();
            }

            _context.Habilidade.Remove(habilidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HabilidadeExists(int id)
        {
            return (_context.Habilidade?.Any(e => e.HabilidadeId == id)).GetValueOrDefault();
        }
    }
}
