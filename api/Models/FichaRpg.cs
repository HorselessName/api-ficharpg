namespace api.Models
{
    public class FichaRpg
    {
        public int IdFichaRpg { get; set; }
        public int Nível { get; set; }
        public string Antecedência { get; set; }
        public string NomeDoJogador { get; set; }
        public string Raça { get; set; }
        public string Alinhamento { get; set; }
        public int PontosDeExperiência { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizado { get; set; } // Alterado para DateTime?
        public bool? Deletado { get; set; }

        // Referenciar um ID para o ModelBuilder - Se relacionar com o Usuario é obrigatório
        public long IdUsuario { get; set; }

        
    }
}
