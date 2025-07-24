using System.ComponentModel.DataAnnotations;

namespace backend.Model.DTO
{
    public class PecaPutDTO
    {
        [Required(ErrorMessage = "O campo Partnumber e obrigatorio.")]
        public string? Partnumber { get; set; }
        [Required(ErrorMessage = "O campo Descricao e obrigatorio.")]
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}