using System.ComponentModel.DataAnnotations;

namespace backend.Model.DTO.Peca
{
    public class PecaPostDTO
    {
        [Required(ErrorMessage = "O campo Partnumber e obrigatorio.")]
        public string? Partnumber { get; set; }
        [Required(ErrorMessage = "O campo Descricao e obrigatorio.")]
        public string? Descricao { get; set; }
    }
}