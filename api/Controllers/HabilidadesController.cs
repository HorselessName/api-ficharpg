    /**
     * Controller responsável por gerenciar as requisições SOMENTE LEITURA referentes a Habilidades
     **/
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using SQLitePCL;

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

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Habilidade>>> GetHabilidades()
        {
            return await _context.Habilidades.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Habilidade>> GetHabilidade(int id)
        {
            var Habilidade = await _context.Habilidades.FindAsync(id);

            if (Habilidade == null)
            {
                return NotFound();
            }

            return Habilidade;
        }

        [HttpPost]
        public async Task<ActionResult<Habilidade>> PostHabilidade(Habilidade habilidade)
        {
            var fichaRpg = await _context.FichasRpg.FindAsync(habilidade.IdFichaRpg);

            if (fichaRpg == null)
            {
                return NotFound("A ficha de RPG com o ID especificado não existe.");
            }

            var habilidadeCriada = _context.Habilidades.Add(habilidade);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHabilidade), new { id = habilidade.IdHabilidade }, habilidade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabilidade(int id, Habilidade habilidade)
        {
            if (id != habilidade.IdHabilidade)
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
                if (!_context.Habilidades.Any(f => f.IdHabilidade == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabilidade(int id)
        {
            var habilidade = await _context.Habilidades.FindAsync(id);
            if (habilidade == null)
            {
                return NotFound();
            }

            _context.Habilidades.Remove(habilidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
