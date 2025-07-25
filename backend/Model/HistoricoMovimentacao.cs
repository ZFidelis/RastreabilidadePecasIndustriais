using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
  public class HistoricoMovimentacao
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PecaId { get; set; }
        public Peca Peca { get; set; }

        public int? EstacaoOrigemId { get; set; }
        public Estacao? EstacaoOrigem { get; set; }
        public int? EstacaoDestinoId { get; set; }
        public Estacao? EstacaoDestino { get; set; }
        
        public DateTime DataMovimentacao { get; set; }
    
        [Required]
        public string Responsavel { get; set; }
        public string Observacao { get; set; }
    }  
}