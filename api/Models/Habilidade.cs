using System.Text.Json.Serialization;

namespace api.Models
{
    public class Habilidade
    {
        public int IdHabilidade { get; set; }
        public string Nome { get; set; }
        public int Pontos { get; set; }
        public int IdFichaRpg { get; set; }
    }
}
