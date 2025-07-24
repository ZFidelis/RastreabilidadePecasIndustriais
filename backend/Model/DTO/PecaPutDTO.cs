using System.ComponentModel.DataAnnotations;

namespace backend.Model.DTO
{
    public class PecaPutDTO
    {
        [Required(ErrorMessage = "O campo Partnumber é obrigatório.")]
        public string? Partnumber { get; set; }
        [Required(ErrorMessage = "O campo Descricao é obrigatório.")]
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}