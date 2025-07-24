using System.componentModel.DataAnnotations;

namespace backend.Model
{
    public class Estacao
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome e obrigatorio.")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O campo Descricao e obrigatorio.")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O campo Invetario e obrigatorio.")]
        public string? Inventario { get; set; }
        public DataTime DataCriacao { get; set; }
        public DataTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; }
    }
}