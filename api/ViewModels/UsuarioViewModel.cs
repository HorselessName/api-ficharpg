using Swashbuckle.AspNetCore.Annotations;

namespace api.ViewModels
{
    /// <summary>
    /// ViewModel Class <c>Usuario</c> é como o objeto é apresentado no Front-End.
    /// </summary>
    public class UsuarioViewModel
    {
        [SwaggerSchema(ReadOnly = true)]
        public long IdUsuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
    }
}