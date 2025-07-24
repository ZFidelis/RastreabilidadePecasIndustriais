using System.ComponentModel.DataAnnotations;

namespace backend.Model.DTO
{
    public class EstacaoPostDTO
    {
        [Required(ErrorMessage = "O campo Nome e obrigatorio.")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O campo Descricao e obrigatorio.")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O campo Invetario e obrigatorio.")]
        public string? Inventario { get; set; }
        public bool Ativo { get; set; }
    }
}