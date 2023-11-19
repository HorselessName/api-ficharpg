namespace api.Models
{
    public class FichaRpg
    {
        public int IdFichaRpg { get; set; }
        public int Nivel { get; set; }
        public string Antecedencia { get; set; }
        public string NomeDoJogador { get; set; }
        public string Raca { get; set; }
        public string Alinhamento { get; set; }
        public int PontosDeExperiencia { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizado { get; set; } // Alterado para DateTime?
        public bool? Deletado { get; set; }

        // Referenciar um ID para o ModelBuilder - Se relacionar com o Usuario é obrigatório
        public long IdUsuario { get; set; }

        
    }
}
