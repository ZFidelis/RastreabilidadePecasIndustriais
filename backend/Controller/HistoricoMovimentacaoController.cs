using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Model;
using backend.Model.DTO.HistoricoMovimentacao;

namespace backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricoMovimentacaoController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        private async Task<Peca?> GetPeca(string partnumber)
        {
            try
            {
                var peca = await _dbContext.tb_peca
                    .Include(p => p.EstacaoAtual)
                    .FirstOrDefaultAsync(p => p.Partnumber == partnumber);

                return peca;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<int?> GetEstacaoAtualId(int pecaId)
        {
            try
            {
                var peca = await _dbContext.tb_peca
                .Include(p => p.EstacaoAtual)
                .FirstOrDefaultAsync(p => p.Id == pecaId);

                return peca?.EstacaoAtual?.Id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<Estacao?> GetEstacao(int estacaoId)
        {
            try
            {
                var estacao = await _dbContext.tb_estacao
                    .Where(e => e.Id == estacaoId)
                    .FirstOrDefaultAsync();

                return estacao;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<int?> OrdemUltimaEstacao()
        {
            try
            {
                return await _dbContext.tb_estacao.OrderByDescending(e => e.Ordem).Select(e => e.Ordem).FirstOrDefaultAsync();

            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<Estacao?> ProximaEstacao(int? ordemAtual)
        {
            if (ordemAtual == null)
            {
                try
                {
                    var estacao = await _dbContext.tb_estacao.OrderBy(e => e.Ordem).FirstOrDefaultAsync();
                    return estacao;
                }
                catch (Exception) {
                    return null;
                } 
            }

            try
            {
                var estacao = await _dbContext.tb_estacao.Where(e => e.Ordem > ordemAtual).OrderBy(e => e.Ordem).FirstOrDefaultAsync();
                return estacao;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public HistoricoMovimentacaoController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("movimentar")]
        public async Task<IActionResult> MovimentarDTO([FromBody] MovimentarDTO movimento)
        {

            try
            {
                if (string.IsNullOrEmpty(movimento.Partnumber))
                {
                    return BadRequest("Partnumber da peca e obrigatorio!");
                }

                var peca = await GetPeca(movimento.Partnumber);

                if (peca == null)
                {
                    return NotFound("Peca nao encontrada!");
                }

                var estacaoOrigemId = peca.EstacaoAtual?.Id;
                var estacaoDestino = await GetEstacao(movimento.EstacaoDestinoId);
                var proximaEstacao = await ProximaEstacao(peca.EstacaoAtual?.Ordem);
                var ultimaEstacao = await OrdemUltimaEstacao();

                if (proximaEstacao?.Ordem == null)
                {
                    return BadRequest("Proxima Estacao nao encontrada!");
                }
                if (proximaEstacao?.Ordem < estacaoDestino?.Ordem)
                {
                    return BadRequest("Movimentacoes de retrocesso nao sao permitidas!");
                }
                if (proximaEstacao?.Ordem > estacaoDestino?.Ordem)
                {
                    return BadRequest("Nao e permitido pular estacoes!");
                }

                if (estacaoDestino?.Ordem == ultimaEstacao.Value)
                {
                    peca.Status = StatusPeca.Finalizada;
                }
                if (estacaoDestino?.Ordem < ultimaEstacao)
                {
                     peca.Status = StatusPeca.EmProcesso;
                }

                var dadosMovimentacao = new HistoricoMovimentacao
                {
                    PecaId = peca.Id,
                    EstacaoOrigemId = estacaoOrigemId,
                    EstacaoDestinoId = movimento.EstacaoDestinoId,
                    DataMovimentacao = DateTime.Now,
                    Responsavel = movimento.Responsavel,
                    Observacao = movimento.Observacao
                };

                peca.EstacaoAtual = estacaoDestino;
                

                _dbContext.tb_historicoMovimentacao.Add(dadosMovimentacao);
                await _dbContext.SaveChangesAsync();

                var reto = estacaoDestino?.Ordem + " -> " + ultimaEstacao.Value;
                return Ok(reto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }


        }

        [HttpGet("{partnumber}")]
        public async Task<ActionResult<IEnumerable<HistoricoMovimentacao>>> GetMovimentoPatnumber(string partnumber)
        {
            try
            {
                var movimentos = await _dbContext.tb_historicoMovimentacao.Include(ed => ed.EstacaoDestino).Include(eo => eo.EstacaoOrigem).Include(p => p.Peca)
                .Where(p => p.Peca.Partnumber == partnumber).ToListAsync();
                if (movimentos == null || !movimentos.Any())
                {
                    return NotFound("Nenhuma Movimentação encontrada!");
                }

                return Ok(movimentos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}