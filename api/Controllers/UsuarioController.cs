using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("api/usuario")]
public class UsuarioController : ControllerBase
{
    private static List<Usuario> usuarios = new List<Usuario>();

    //GET: api/produto/listar
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar() =>
        usuarios.Count == 0 ? NotFound() : Ok(usuarios);

    //GET: api/produto/buscar/{bolacha}
    [HttpGet]
    [Route("buscar/{nome}")]
    public IActionResult Buscar([FromRoute] string nome)
    {
        foreach (Usuario usuarioCadastrado in usuarios)
        {
            if (usuarioCadastrado.Nome == nome)
            {
                return Ok(usuarioCadastrado);
            }
        }
        return NotFound();
    }

    //POST: api/usuario/cadastrar
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Usuario usuario)
    {
        usuarios.Add(usuario);
        return Created("", usuario);
    }

}
