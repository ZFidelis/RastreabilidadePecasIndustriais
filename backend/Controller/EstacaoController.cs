using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Model;
using backend.Model.DTO.Estacao;
using backend.Shared.Constants;

namespace backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacaoController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public EstacaoController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddEstacao(EstacaoPostDTO estacaoDTO)
        {
            if (estacaoDTO == null || estacaoDTO.Inventario == null)
            {
                return BadRequest("Dados Invalidos!");
            }
            
            if (estacaoDTO.Ordem <= 0)
            {
                return BadRequest(ErrorMessages.OrdemMenorIgualZero);    
            }

            try
            {
                if ((await InventarioExiste(estacaoDTO.Inventario)).Value)
                {
                    return BadRequest("Inventario ja esta sendo utilizado!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            var ordemExistente = await _dbContext.tb_estacao.AnyAsync(e => e.Ordem == estacaoDTO.Ordem);

            if (ordemExistente)
            {
                return BadRequest(ErrorMessages.OrdemExiste);
            }
            

            try
            {
                var estacao = new Estacao
                {
                    Nome = estacaoDTO.Nome,
                    Descricao = estacaoDTO.Descricao,
                    Inventario = estacaoDTO.Inventario,
                    Ordem = estacaoDTO.Ordem,
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = null
                };

                _dbContext.tb_estacao.Add(estacao);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEstacaoById), new { id = estacao.Id }, estacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estacao>>> GetEstacoes()
        {
            try
            {
                var estacoes = await _dbContext.tb_estacao.ToListAsync();
                if (estacoes == null || !estacoes.Any())
                {
                    return NotFound("Nenhuma Estacao encontrada!");
                }

                return Ok(estacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estacao>> GetEstacaoById(int id)
        {
            try
            {
                var estacao = await _dbContext.tb_estacao.FindAsync(id);
                if (estacao == null)
                {
                    return NotFound("Estacao nao encontrada!");
                }

                return Ok(estacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstacaoById(int id, [FromBody] EstacaoPutDTO estacaoDTO)
        {
            if (estacaoDTO == null || id <= 0 || estacaoDTO.Inventario == null)
            {
                return BadRequest("Dados Invalidos!");
            }

            try
            {
                if ((await InventarioExiste(estacaoDTO.Inventario)).Value)
                {
                    return BadRequest("Inventario ja esta sendo utilizado!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            if (estacaoDTO.Ordem <= 0)
            {
                return BadRequest(ErrorMessages.OrdemMenorIgualZero);
            }
            
            try
            {
                var estacaoAtual = await _dbContext.tb_estacao.FindAsync(id);

                if (estacaoAtual == null)
                {
                    return NotFound("Estacao nao encontrada!");
                }


                if (estacaoDTO.Ordem != estacaoAtual.Ordem)
                {
                    var ordemExistente = await _dbContext.tb_estacao.AnyAsync(e => e.Ordem == estacaoDTO.Ordem);

                    if (ordemExistente)
                    {
                        return BadRequest(ErrorMessages.OrdemExiste);
                    }
                }

                estacaoAtual.Nome = estacaoDTO.Nome;
                estacaoAtual.Descricao = estacaoDTO.Descricao;
                estacaoAtual.Inventario = estacaoDTO.Inventario;
                estacaoAtual.Ordem = estacaoDTO.Ordem;
                estacaoAtual.Ativo = estacaoDTO.Ativo;
                estacaoAtual.DataAtualizacao = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                return Ok(estacaoAtual);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstacaoById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID invalido!");
            }

            try
            {
                var estacao = await _dbContext.tb_estacao.FindAsync(id);

                if (estacao == null)
                {
                    return NotFound("Estacao nao encontrada!");
                }

                _dbContext.tb_estacao.Remove(estacao);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("inventario-existe/{inventario}")]
        public async Task<ActionResult<bool>> InventarioExiste(string inventario)
        {
            try
            {
                var peca = await _dbContext.tb_estacao.FirstOrDefaultAsync(p => p.Inventario == inventario);
                return peca != null;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}