using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class Estacao
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Invetario e obrigatorio.")]
        public string? Inventario { get; set; }
        [Required(ErrorMessage = "O campo Nome e obrigatorio.")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O campo Descricao e obrigatorio.")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O campo Ordem e obrigatorio.")]
        public int Ordem { get; set; }

        // Controlados pelo sistema
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; }
    }
}