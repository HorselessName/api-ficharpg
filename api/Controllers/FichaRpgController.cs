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
        private const string USER_NOT_FOUND_MESSAGE = "Nenhum usuário encontrado com a ID informada.";

        public FichasRpgController(AppDataContext context)
        {
            _context = context;
        }

        // Listar todas as Fichas -- GET: api/FichasRpg/1/listar
        [HttpGet("{idUsuario}/listar")]
        public async Task<ActionResult<IList<FichaRpgViewModel>>> GetFichasRpgAsync(long idUsuario)
        {
            UsuarioModel usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == idUsuario && !(u.Deletado ?? true));

            if (usuario == null)
            {
                return NotFound(new { message = USER_NOT_FOUND_MESSAGE });
            }

            List<FichaRpgViewModel> fichas = await _context.FichasRpg
                        .Where(f => f.IdUsuario == idUsuario && !(f.Deletado ?? true))
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

            return !fichas.Any()
                ? (ActionResult<IList<FichaRpgViewModel>>)Ok(new { message = $"O astuto {usuario.Nome} (ID: {idUsuario}) ainda não se aventurou no mundo do RPG por aqui! 😲" })
                : (ActionResult<IList<FichaRpgViewModel>>)Ok(fichas);
        }

        // Listar Ficha do Usuario -- GET: api/FichasRpg/1/listar/1
        [HttpGet("{idUsuario}/listar/{idFicha}")]
        public async Task<ActionResult<FichaRpgViewModel>> GetFichaRpgAsync(long idUsuario, long idFicha)
        {
            UsuarioModel usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == idUsuario && !(u.Deletado ?? true));

            if (usuario == null)
            {
                return NotFound(new { message = USER_NOT_FOUND_MESSAGE });
            }

            FichaRpgModel ficha = await _context.FichasRpg
                                .FirstOrDefaultAsync(f => f.IdUsuario == idUsuario && f.IdFichaRpg == idFicha && !(f.Deletado ?? true));

            return ficha == null
                ? (ActionResult<FichaRpgViewModel>)NotFound(new { message = $"Poxa, parece que essa Ficha de RPG (ID: {idFicha}) se escondeu do {usuario.Nome} (ID: {idUsuario}) ou foi transportada para outra dimensão. 🌌" })
                : (ActionResult<FichaRpgViewModel>)Ok(new FichaRpgViewModel
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

        // Cadastrar Ficha -- POST: api/FichasRpg/1/criar
        [HttpPost("{idUsuario}/criar")]
        public async Task<ActionResult<object>> PostFichaRpg(long idUsuario, FichaRpgViewModel fichaInput)
        {
            UsuarioModel usuario = await _context.Usuarios.FindAsync(idUsuario);

            if (usuario is null)
            {
                return NotFound(new { message = USER_NOT_FOUND_MESSAGE });
            }

            if (AreAnyNullOrEmpty(
                fichaInput.NomeDoJogador,
                fichaInput.Raça,
                fichaInput.Alinhamento))
            {
                return BadRequest(new
                {
                    message = "Nome do Jogador, Raça e Alinhamento são obrigatórios!"
                });
            }

            if (!fichaInput.Nível.HasValue || !fichaInput.PontosDeExperiência.HasValue)
            {
                return BadRequest(new
                {
                    message = "Nível e Pontos de Experiência são obrigatórios!"
                });
            }

            FichaRpgModel ficha = new()
            {
                Nível = fichaInput.Nível.Value,
                Antecedente = fichaInput.Antecedente,
                NomeDoJogador = fichaInput.NomeDoJogador,
                Raça = fichaInput.Raça,
                Alinhamento = fichaInput.Alinhamento,
                PontosDeExperiência = fichaInput.PontosDeExperiência.Value,
                IdUsuario = idUsuario
            };

            _ = _context.FichasRpg.Add(ficha);
            _ = await _context.SaveChangesAsync();

            return CreatedAtAction(
                actionName: nameof(GetFichaRpgAsync),
                routeValues: new { idUsuario, idFicha = ficha.IdFichaRpg },
                value: new
                {
                    message = "A Ficha de RPG foi criada com maestria! 🎲",
                    ficha
                });
        }

        // Atualizar Ficha -- PUT: api/FichasRpg/1/atualizar/1
        [HttpPut("{idUsuario}/atualizar/{idFicha}")]
        public async Task<IActionResult> PutFichaRpg(long idUsuario, long idFicha, FichaRpgViewModel fichaInput)
        {
            UsuarioModel usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario is null)
            {
                return NotFound(new { message = USER_NOT_FOUND_MESSAGE });
            }

            FichaRpgModel existingFicha = await _context.FichasRpg.FirstOrDefaultAsync(f => f.IdFichaRpg == idFicha && f.Deletado != true && f.IdUsuario == idUsuario);
            if (existingFicha == null)
            {
                return NotFound(new { message = FICHA_NOT_FOUND_MESSAGE });
            }

            if (!fichaInput.Nível.HasValue || !fichaInput.PontosDeExperiência.HasValue)
            {
                return BadRequest(new { message = "Nível e Pontos de Experiência são obrigatórios!" });
            }

            existingFicha.Nível = fichaInput.Nível.Value;
            existingFicha.Antecedente = fichaInput.Antecedente ?? existingFicha.Antecedente;
            existingFicha.NomeDoJogador = fichaInput.NomeDoJogador ?? existingFicha.NomeDoJogador;
            existingFicha.Raça = fichaInput.Raça ?? existingFicha.Raça;
            existingFicha.Alinhamento = fichaInput.Alinhamento ?? existingFicha.Alinhamento;
            existingFicha.PontosDeExperiência = fichaInput.PontosDeExperiência.Value;
            existingFicha.DataAtualizado = DateTime.Now;

            _context.Entry(existingFicha).State = EntityState.Modified;

            try
            {
                _ = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Ocorreu um erro ao atualizar a ficha de RPG. Talvez um dragão tenha atravessado seu caminho... 🐉" });
            }

            return Ok(new { message = "Ficha de RPG atualizada com sucesso! Agora vá vencer alguns orcs! 🗡️", ficha = existingFicha });
        }

        // Deletar Ficha -- DELETE: api/FichasRpg/1/1
        [HttpDelete("{idUsuario}/{idFicha}")]
        public async Task<IActionResult> DeleteFichaRpg(long idUsuario, long idFicha)
        {
            UsuarioModel usuario = await _context.Usuarios.FindAsync(idUsuario);

            if (usuario is null)
            {
                return NotFound(new { message = USER_NOT_FOUND_MESSAGE });
            }

            FichaRpgModel ficha = await _context.FichasRpg.FirstOrDefaultAsync(f => f.IdFichaRpg == idFicha && f.IdUsuario == idUsuario);

            if (ficha is null)
            {
                return !_context.FichasRpg.Any(f => f.IdUsuario == idUsuario)
                    ? BadRequest(new
                    {
                        message = $"O usuário {usuario.Nome} (ID: {idUsuario}) não possui fichas de RPG para serem deletadas."
                    })
                    : NotFound(new
                    {
                        message = $"Não foram encontradas fichas para o usuário {usuario.Nome} com o ID ({idUsuario}) fornecido."
                    });
            }

            if (ficha.Deletado.HasValue && ficha.Deletado.Value)
            {
                return BadRequest(new { message = $"A ficha de RPG com ID {idFicha} já foi deletada." });
            }

            ficha.Deletado = true;
            ficha.DataAtualizado = DateTime.Now;

            _context.Entry(ficha).State = EntityState.Modified;
            _ = await _context.SaveChangesAsync();

            return Ok(new { message = $"A ficha de RPG com ID {idFicha} foi deletada com sucesso." });
        }

        public static bool AreAnyNullOrEmpty(params string[] values)
        {
            return values.Any(string.IsNullOrEmpty);
        }

    }
}