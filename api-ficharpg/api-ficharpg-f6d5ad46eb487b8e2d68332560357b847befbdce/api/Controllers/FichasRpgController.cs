using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using SQLitePCL;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichasRpgController : ControllerBase
    {
        private readonly AppDataContext _context;

        public FichasRpgController(AppDataContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<FichaRpg>>> GetFichasRpg()
        {
            return await _context.FichasRpg.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FichaRpg>> GetFichaRpg(int id)
        {
            var fichaRpg = await _context.FichasRpg.FindAsync(id);

            if (fichaRpg == null)
            {
                return NotFound();
            }

            return fichaRpg;
        }


        [HttpPost]
        public async Task<ActionResult<FichaRpg>> PostFichaRpg(FichaRpg fichaRpg)
        {
              try
                {
                    _context.FichasRpg.Add(fichaRpg);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetFichaRpg), new { id = fichaRpg.IdFichaRpg }, fichaRpg);
                }
                catch (DbUpdateException dbEx)
                {  
                    LogError(dbEx); 
                    return StatusCode(500, "Um erro ocorreu ao acessar o banco de dados.");
                }
                catch (Exception ex)
                {
                    LogError(ex); 
                    return StatusCode(500, "Ocorreu um erro interno no servidor.");
                }
        }

        private void LogError(Exception ex)
            {
                // Exibe a exceção no terminal
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
                Console.WriteLine("Detalhes da Exceção: ");
                Console.WriteLine(ex.ToString());
            }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutFichaRpg(int id, FichaRpg fichaRpg)
        {
            if (id != fichaRpg.IdFichaRpg)
            {
                return BadRequest();
            }

            _context.Entry(fichaRpg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.FichasRpg.Any(f => f.IdFichaRpg == id))
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
        public async Task<IActionResult> DeleteFichaRpg(int id)
        {
            var fichaRpg = await _context.FichasRpg.FindAsync(id);
             
            if (fichaRpg == null)
            {
                return NotFound();
            }

            List<Habilidade> listaHabilidades = await _context.Habilidades.ToListAsync();
            
            foreach(Habilidade habilidade in listaHabilidades){
                if(habilidade.IdFichaRpg == id){
                    await DeleteHabilidade(habilidade.IdFichaRpg);
                }
            }

            _context.FichasRpg.Remove(fichaRpg);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        protected async Task<IActionResult> DeleteHabilidade(int id)
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
