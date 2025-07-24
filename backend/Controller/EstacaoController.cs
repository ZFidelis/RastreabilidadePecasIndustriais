using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Model;
using backend.Model.DTO;

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
            if (estacaoDTO == null)  {
                return BadRequest("Dados Invalidos!");
            }

            try {
                var estacao = new Estacao {
                    Nome = estacaoDTO.Nome,
                    Descricao = estacaoDTO.Descricao,
                    Inventario = estacaoDTO.Inventario,
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = null
                };

                _dbContext.tb_estacao.Add(estacao);
                await _dbContext.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetEstacaoById), new { id = estacao.Id }, estacao);
            }
            catch (Exception ex)  {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estacao>>> GetEstacoes()
        {   
            try {
                var estacoes = await _dbContext.tb_estacao.ToListAsync();
                if (estacoes == null || !estacoes.Any()) {
                    return NotFound("Nenhuma Estacao encontrada!");
                }

                return Ok(estacoes);
            }
            catch (Exception ex) {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estacao>> GetEstacaoById(int id)
        {   
            try {
                var estacao = await _dbContext.tb_estacao.FindAsync(id);
                if (estacao == null) {
                    return NotFound("Estacao nao encontrada!");
                }

                return Ok(estacao);
            }
            catch (Exception ex) {
                return StatusCode(500, ex);
            }
        }

    }
}