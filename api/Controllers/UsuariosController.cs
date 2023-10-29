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
    public class UsuariosController : ControllerBase
    {
        private readonly AppDataContext _context;
        private const string USER_NOT_FOUND_MESSAGE = "Usuário não encontrado.";

        public UsuariosController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioViewModel>>> GetUsuariosAsync()
        {
            var usuarios = await _context.Usuarios
                .Where(u => !(u.Deletado ?? true))
                .Select(u => new UsuarioViewModel
                {
                    IdUsuario = u.IdUsuario,
                    Nome = u.Nome,
                    Email = u.Email
                }).ToListAsync();

            return !usuarios.Any()
                ? Ok(new { message = "Nenhum usuário cadastrado." })
                : Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioViewModel>> GetUsuarioAsync(long id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id && !(u.Deletado ?? true));

            return usuario != null
                ? Ok(new UsuarioViewModel
                {
                    IdUsuario = usuario.IdUsuario,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                })
                : NotFound(new { message = USER_NOT_FOUND_MESSAGE });
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> PostUsuario(UsuarioViewModel userInput)
        {
            UsuarioModel usuario = new()
            {
                Nome = userInput.Nome,
                Email = userInput.Email
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(long id, UsuarioViewModel userInput)
        {
            var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id && !(u.Deletado ?? true));

            if (existingUser == null) return NotFound(new { message = USER_NOT_FOUND_MESSAGE });

            existingUser.Nome = userInput.Nome;
            existingUser.Email = userInput.Email;
            existingUser.DataAtualizado = DateTime.Now;
            _context.Entry(existingUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(new { message = USER_NOT_FOUND_MESSAGE });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id && !(u.Deletado ?? true));

            if (usuario is null) return NotFound(new { message = USER_NOT_FOUND_MESSAGE });

            usuario.Deletado = true;
            usuario.DataAtualizado = DateTime.Now;
            _context.Entry(usuario).State = EntityState.Modified;

            _ = await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}