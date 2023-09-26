namespace api.Models
{
    public class Habilidade
    {
        public int HabilidadeId { get; set; }
        public string Nome { get; set; }
        public int Pontos { get; set; }

        // Relacionamento: Uma habilidade pertence a uma ficha de RPG.
        public int FichaRpgId { get; set; }
        public FichaRpg FichaRpg { get; set; }
    }
}
