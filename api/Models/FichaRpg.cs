namespace api.Models
{
    public class FichaRpg
    {
        public int FichaRpgId { get; set; }
        public int Nível { get; set; }

        public string Antecedência { get; set; }
        public string NomeDoJogador { get; set; }
        public string NomeDoPersonagem { get; set; }

        public string Raça { get; set; }
        public string Alinhamento { get; set; }
        public int PontosDeExperiência { get; set; }

        public List<Habilidade> Habilidades { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizado { get; set; }
        public bool? Deletado { get; set; }

        // Relacionamento muitos para um - Muitas fichas podem pertencer a um usuario.
        public Usuario Usuario { get; set; }
    }
}
