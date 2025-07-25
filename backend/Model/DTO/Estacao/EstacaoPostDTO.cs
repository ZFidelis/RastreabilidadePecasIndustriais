using System.ComponentModel.DataAnnotations;

namespace backend.Model.DTO.Estacao
{
    public class EstacaoPostDTO
    {
        [Required(ErrorMessage = "O campo Nome e obrigatorio.")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O campo Descricao e obrigatorio.")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O campo Invetario e obrigatorio.")]
        public string? Inventario { get; set; }
        [Required(ErrorMessage = "O campo Ordem e obrigatorio.")]
        public int Ordem { get; set; }
    }
}