using System.ComponentModel.DataAnnotations;

namespace backend.Model.DTO.HistoricoMovimentacao
{
    public class MovimentarDTO
    {
        public string Partnumber {get; set; }
        public int EstacaoDestinoId { get; set; }
        public string Responsavel { get; set; }
        public string Observacao { get; set; }
    }
}