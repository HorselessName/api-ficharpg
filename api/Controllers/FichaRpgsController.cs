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
    public class FichaRpgsController : ControllerBase
    {
        private readonly AppDataContext _context;

        public FichaRpgsController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/FichaRpgs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FichaRpg>>> GetFichaRpg()
        {
          if (_context.FichaRpg == null)
          {
              return NotFound();
          }
            return await _context.FichaRpg.ToListAsync();
        }

        // GET: api/FichaRpgs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FichaRpg>> GetFichaRpg(int id)
        {
          if (_context.FichaRpg == null)
          {
              return NotFound();
          }
            var fichaRpg = await _context.FichaRpg.FindAsync(id);

            if (fichaRpg == null)
            {
                return NotFound();
            }

            return fichaRpg;
        }

        // PUT: api/FichaRpgs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFichaRpg(int id, FichaRpg fichaRpg)
        {
            if (id != fichaRpg.FichaRpgId)
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
                if (!FichaRpgExists(id))
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

        // POST: api/FichaRpgs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FichaRpg>> PostFichaRpg(FichaRpg fichaRpg)
        {
          if (_context.FichaRpg == null)
          {
              return Problem("Entity set 'AppDataContext.FichaRpg'  is null.");
          }
            _context.FichaRpg.Add(fichaRpg);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFichaRpg", new { id = fichaRpg.FichaRpgId }, fichaRpg);
        }

        // DELETE: api/FichaRpgs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFichaRpg(int id)
        {
            if (_context.FichaRpg == null)
            {
                return NotFound();
            }
            var fichaRpg = await _context.FichaRpg.FindAsync(id);
            if (fichaRpg == null)
            {
                return NotFound();
            }

            _context.FichaRpg.Remove(fichaRpg);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FichaRpgExists(int id)
        {
            return (_context.FichaRpg?.Any(e => e.FichaRpgId == id)).GetValueOrDefault();
        }
    }
}
