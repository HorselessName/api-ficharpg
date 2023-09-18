namespace api.Models
{
    public class Usuario
    {

        // Construtor para sempre atribuir a data de criacao do objeto
        public Usuario() { 
            DataCriacao = DateTime.Now;
        }

        // Criar as propriedades de forma abreviada para nossa tabela.
        public int UsuarioId { get; set; }
        public string? Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizado { get; set; }
        public bool? Deletado { get; set; }
        public string? Email { get; set; }
    }
}
