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
        public async Task<ActionResult<IList<UsuarioViewModel>>> GetUsuariosAsync()
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

        // Listar Usuario -- GET: api/Usuarios/1
        [HttpGet("{id}")]
        public async Task<ActionResult<IList<UsuarioViewModel>>> GetUsuarioAsync(long id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id && !(u.Deletado ?? true));

            if (usuario == null)
                return NotFound(new { message = USER_NOT_FOUND_MESSAGE });

            return Ok(new UsuarioViewModel
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email
            });
        }

        // Cadastrar Usuario -- POST : api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> PostUsuario(UsuarioViewModel userInput)
        {
            // Verificando se os campos Nome e Email foram fornecidos
            if (AreAnyNullOrEmpty(
                userInput.Nome,
                userInput.Email)) return BadRequest(new
                {
                    message = "Nome e Email são obrigatórios!"
                }
                );

            UsuarioModel usuario = new()
            {
                Nome = userInput.Nome,
                Email = userInput.Email
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                actionName: nameof(GetUsuarioAsync),
                routeValues: new { id = usuario.IdUsuario },
                value: new
                {
                    message = "O usuário foi cadastrado com sucesso.",
                    user = usuario
                });
        }

        // Atualizar Cadastro -- PUT : api/Usuarios/1
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutUsuario(long id, UsuarioViewModel userInput)
        {
            var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id && !(u.Deletado ?? true));

            if (existingUser == null)
                return NotFound(new { message = USER_NOT_FOUND_MESSAGE });

            string updatedFields = "";

            // Atualização condicional dos campos
            if (!string.IsNullOrEmpty(userInput.Nome) && existingUser.Nome != userInput.Nome)
            {
                existingUser.Nome = userInput.Nome;
                updatedFields += "Nome ";
            }

            if (!string.IsNullOrEmpty(userInput.Email) && existingUser.Email != userInput.Email)
            {
                existingUser.Email = userInput.Email;
                updatedFields += "Email ";
            }

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

            return Ok(new
            {
                message = string.IsNullOrWhiteSpace(updatedFields) ?
                            "Nenhum campo foi atualizado." :
                            $"Campo(s) atualizado(s): {updatedFields.Trim()}.",
                user = existingUser
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario is null)
                return NotFound(new { message = USER_NOT_FOUND_MESSAGE });

            if (usuario.Deletado.HasValue && usuario.Deletado.Value)
                return BadRequest(new { 
                    message = $"O usuário {usuario.Nome} (ID: {usuario.IdUsuario}) já foi deletado." }
                );

            usuario.Deletado = true;
            usuario.DataAtualizado = DateTime.Now;
            _context.Entry(usuario).State = EntityState.Modified;

            _ = await _context.SaveChangesAsync();

            return Ok(new { 
                message = $"O usuário {usuario.Nome} (ID: {usuario.IdUsuario}) foi deletado com sucesso." 
            });
        }

        public static bool AreAnyNullOrEmpty(params string[] values)
        {
            return values.Any(string.IsNullOrEmpty);
        }

    }
}