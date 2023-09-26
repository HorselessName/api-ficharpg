using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Usuario
    {
        // ##### Propriedades Bloqueadas no Request #####
        [NotMapped]
        public long IdUsuario { get; internal set; }

        [NotMapped]
        public DateTime DataCriacao { get; set; }

        [NotMapped]
        public DateTime? DataAtualizado { get; set; }

        [NotMapped]
        public bool? Deletado { get; set; }

        // ##### Propriedades para API #####
        [Required] // Garante que o Nome seja fornecido
        public string Nome { get; set; }

        [Required] // Garante que o Email seja fornecido
        public string Email { get; set; }
        
    }
}