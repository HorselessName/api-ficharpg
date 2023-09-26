namespace api.Models
{
    public class Usuario
    {
        // Construtor para sempre atribuir a data de criação do objeto
        public Usuario()
        {
            DataCriacao = DateTime.Now;
        }

        // Criar as propriedades de forma abreviada para nossa tabela.
        public int UsuarioId { get; set; }
        public string? Nome { get; set; }

        // As propriedades abaixo serão usadas para sempre que houver uma
        // criação ou atualização neste modelo.
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizado { get; set; } // Alterado para DateTime?

        // Essa propriedade vai servir para ao invés do usuário ser deletado,
        // ele vai ser marcado como deletado apenas.
        public bool? Deletado { get; set; }

        public string? Email;
    }
}