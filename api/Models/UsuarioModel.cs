#nullable disable

using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    /// <summary>
    /// Model Class <c>Usuario</c> é como o objeto é representado e cadastrado no banco de dados.
    /// </summary>
    public class UsuarioModel
    {
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizado { get; set; }
        public bool? Deletado { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdUsuario { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        public UsuarioModel()
        {
            DataCriacao = DateTime.Now;
            Deletado = false;
        }
    }
}