using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers
{
    /**
     * Controller responsável por gerenciar as requisições SOMENTE LEITURA referentes a Habilidades
     **/
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
        public async Task<ActionResult<IEnumerable<Habilidade>>> GetHabilidades()
        {
          if (_context.Habilidades == null)
          {
              return NotFound();
          }
            return await _context.Habilidades.ToListAsync();
        }

        private bool HabilidadeExists(int id)
        {
            return (_context.Habilidades?.Any(e => e.IdHabilidade == id)).GetValueOrDefault();
        }
    }
}
