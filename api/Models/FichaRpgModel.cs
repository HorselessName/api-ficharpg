using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    /// <summary>
    /// Model Class <c>FichaRpgModel</c> representa a ficha de RPG no banco de dados.
    /// </summary>
    public class FichaRpgModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdFichaRpg { get; set; }

        [Required]
        public int Nível { get; set; }

        public string Antecedente { get; set; }

        [Required]
        public string NomeDoJogador { get; set; }

        [Required]
        public string Raça { get; set; }

        public string Alinhamento { get; set; }

        [Required]
        public int PontosDeExperiência { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizado { get; set; }
        public bool? Deletado { get; set; }

        // Relacionamento com UsuarioModel
        [Required]
        [ForeignKey("Usuario")]
        public long IdUsuario { get; set; }

        [Required]
        public virtual UsuarioModel Usuario { get; set; }

        public FichaRpgModel()
        {
            DataCriacao = DateTime.Now;
            Deletado = false;
        }
    }
}
