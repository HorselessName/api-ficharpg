using Swashbuckle.AspNetCore.Annotations;

namespace api.ViewModels
{
    public class FichaRpgViewModel
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdFichaRpg { get; set; }
        public int Nível { get; set; }
        public string Antecedente { get; set; }
        public string NomeDoJogador { get; set; }
        public string Raça { get; set; }
        public string Alinhamento { get; set; }
        public int PontosDeExperiência { get; set; }
        public long IdUsuario { get; set; }
    }
}