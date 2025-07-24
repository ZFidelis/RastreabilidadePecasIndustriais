using System.ComponentModel.DataAnnotations;

namespace backend.Model.DTO
{
    public class PecaPostDTO
    {
        [Required(ErrorMessage = "O campo Partnumber é obrigatório.")]
        public string? Partnumber { get; set; }
        [Required(ErrorMessage = "O campo Descricao é obrigatório.")]
        public string? Descricao { get; set; }
    }
}