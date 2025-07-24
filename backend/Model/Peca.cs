using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Peca
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Partnumber e obrigatorio.")]
        public string? Partnumber { get; set; }
        [Required(ErrorMessage = "O campo Descricao e obrigatorio.")]
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; }
    }
}