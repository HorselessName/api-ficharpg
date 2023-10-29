#nullable disable

using api.Data;
using api.Models;
using api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichasRpgController : ControllerBase
    {
        private readonly AppDataContext _context;
        private const string FICHA_NOT_FOUND_MESSAGE = "Ficha de RPG não encontrada.";

        public FichasRpgController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/FichasRpg
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FichaRpgViewModel>>> GetFichasRpgAsync()
        {
            var fichas = await _context.FichasRpg
                .Where(f => f.Deletado != true)
                .Select(f => new FichaRpgViewModel
                {
                    IdFichaRpg = f.IdFichaRpg,
                    Nível = f.Nível,
                    Antecedente = f.Antecedente,
                    NomeDoJogador = f.NomeDoJogador,
                    Raça = f.Raça,
                    Alinhamento = f.Alinhamento,
                    PontosDeExperiência = f.PontosDeExperiência
                }).ToListAsync();

            if (!fichas.Any())
            {
                return Ok(new { message = "Nenhuma ficha de RPG cadastrada." });
            }
            return Ok(fichas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FichaRpgViewModel>> GetFichaRpgAsync(int id)
        {
            var ficha = await _context.FichasRpg.FirstOrDefaultAsync(f => f.IdFichaRpg == id && f.Deletado != true);

            if (ficha == null)
            {
                return NotFound(new { message = FICHA_NOT_FOUND_MESSAGE });
            }

            return Ok(new FichaRpgViewModel
            {
                IdFichaRpg = ficha.IdFichaRpg,
                Nível = ficha.Nível,
                Antecedente = ficha.Antecedente,
                NomeDoJogador = ficha.NomeDoJogador,
                Raça = ficha.Raça,
                Alinhamento = ficha.Alinhamento,
                PontosDeExperiência = ficha.PontosDeExperiência
            });
        }

        [HttpPost]
        public async Task<ActionResult<FichaRpgModel>> PostFichaRpg(FichaRpgViewModel fichaInput)
        {
            FichaRpgModel ficha = new()
            {
                Nível = fichaInput.Nível,
                Antecedente = fichaInput.Antecedente,
                NomeDoJogador = fichaInput.NomeDoJogador,
                Raça = fichaInput.Raça,
                Alinhamento = fichaInput.Alinhamento,
                PontosDeExperiência = fichaInput.PontosDeExperiência,
                IdUsuario = fichaInput.IdUsuario
            };

            await _context.FichasRpg.AddAsync(ficha);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFichaRpg", new { id = ficha.IdFichaRpg }, ficha);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFichaRpg(int id, FichaRpgViewModel fichaInput)
        {
            var existingFicha = await _context.FichasRpg.FirstOrDefaultAsync(f => f.IdFichaRpg == id && f.Deletado != true);

            if (existingFicha == null)
            {
                return NotFound(new { message = FICHA_NOT_FOUND_MESSAGE });
            }

            existingFicha.Nível = fichaInput.Nível;
            existingFicha.Antecedente = fichaInput.Antecedente;
            existingFicha.NomeDoJogador = fichaInput.NomeDoJogador;
            existingFicha.Raça = fichaInput.Raça;
            existingFicha.Alinhamento = fichaInput.Alinhamento;
            existingFicha.PontosDeExperiência = fichaInput.PontosDeExperiência;
            existingFicha.DataAtualizado = DateTime.Now;

            _context.Entry(existingFicha).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Ocorreu um erro ao atualizar a ficha de RPG." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFichaRpg(int id)
        {
            var ficha = await _context.FichasRpg.FirstOrDefaultAsync(f => f.IdFichaRpg == id && f.Deletado != true);

            if (ficha is null)
            {
                return NotFound(new { message = FICHA_NOT_FOUND_MESSAGE });
            }

            ficha.Deletado = true;
            ficha.DataAtualizado = DateTime.Now;

            _context.Entry(ficha).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}