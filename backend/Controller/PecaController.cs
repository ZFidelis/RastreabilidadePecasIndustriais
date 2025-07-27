using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Model;
using backend.Model.DTO.Peca;

namespace backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PecaController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public PecaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddPeca(PecaPostDTO pecaDTO)
        {
            if (pecaDTO == null)
            {
                return BadRequest("Dados Invalidos!");
            }

            try
            {
                var existe = (await PartnumberExiste(pecaDTO.Partnumber)).Value;
                if (existe)
                {
                    return BadRequest("Partnumber ja esta sendo utilizado!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            try
            {
                var peca = new Peca
                {
                    Partnumber = pecaDTO.Partnumber,
                    Descricao = pecaDTO.Descricao,
                    Status = StatusPeca.Pendente,
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = null
                };

                _dbContext.tb_peca.Add(peca);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPecaById), new { id = peca.Id }, peca);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peca>>> GetPecas()
        {
            try
            {
                var pecas = await _dbContext.tb_peca.ToListAsync();
                if (pecas == null || !pecas.Any())
                {
                    return NotFound("Nenhuma Peca encontrada!");
                }

                return Ok(pecas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Peca>> GetPecaById(int id)
        {
            try
            {
                var peca = await _dbContext.tb_peca.FindAsync(id);
                if (peca == null)
                {
                    return NotFound("Peca nao encontrada!");
                }

                return Ok(peca);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePecaById(int id, [FromBody] PecaPutDTO pecaDTO)
        {
            if (pecaDTO == null || id <= 0)
            {
                return BadRequest("Dados Invalidos!");
            }

            try
            {
                var pecaAtual = await _dbContext.tb_peca.FindAsync(id);

                if (pecaAtual == null)
                {
                    return NotFound("Peca nao encontrada!");
                }

                pecaAtual.Partnumber = pecaDTO.Partnumber;
                pecaAtual.Descricao = pecaDTO.Descricao;
                pecaAtual.Ativo = pecaDTO.Ativo;
                pecaAtual.DataAtualizacao = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                return Ok(pecaAtual);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePecaById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID invalido!");
            }

            try
            {
                var peca = await _dbContext.tb_peca.FindAsync(id);

                if (peca == null)
                {
                    return NotFound("Peca nao encontrada!");
                }

                _dbContext.tb_peca.Remove(peca);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("partnumber-existe/{partnumber}")]
        public async Task<ActionResult<bool>> PartnumberExiste(string partnumber)
        {
            try
            {
                var peca = await _dbContext.tb_peca.FirstOrDefaultAsync(p => p.Partnumber == partnumber);
                return peca != null;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}