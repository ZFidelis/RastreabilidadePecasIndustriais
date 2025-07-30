using backend.Controller;
using backend.Data;
using backend.Model;
using backend.Model.DTO.Estacao;
using backend.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.UnitTests.Controllers
{
    public class EstacaoControllerTests
    {
        private readonly AppDbContext _dbContext;
        private readonly EstacaoController _controller;

        public EstacaoControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid().ToString()).Options;
            _dbContext = new AppDbContext(options);
            _controller = new EstacaoController(_dbContext);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _dbContext.Database.EnsureDeleted();

            var estacoes = new List<Estacao>
            {
                new Estacao
                {
                    Id = 1,
                    Inventario = "InventarioUnico1",
                    Nome = "Estacao 1 Teste",
                    Descricao = "Estacao para Testes Unitarios",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    Ordem = 1
                },
                new Estacao
                {
                    Id = 2,
                    Inventario = "InventarioUnico2",
                    Nome = "Estacao 2 Teste",
                    Descricao = "Estacao para Testes Unitarios",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    Ordem = 2
                }
            };
            _dbContext.tb_estacao.AddRange(estacoes);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task AddEstacao_ErroQuandoOrdemExiste()
        {
            var estacaoDTO = new EstacaoPostDTO
            {
                Nome = "Estacao Teste",
                Descricao = "Estacao de Teste",
                Inventario = "Inventario de Teste",
                Ordem = 2
            };

            var result = await _controller.AddEstacao(estacaoDTO);

            var _requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(ErrorMessages.OrdemExiste, _requestResult.Value);
        }

        [Fact]
        public async Task AddEstacao_CreatedQuandoOrdemNaoExiste()
        {
            var estacaoDTO = new EstacaoPostDTO
            {
                Nome = "Estacao Teste",
                Descricao = "Estacao de Teste",
                Inventario = "Inventario de Teste",
                Ordem = 3
            };

            var result = await _controller.AddEstacao(estacaoDTO);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task AddEstacao_ErroQuandoOrdemMenorIgualZero()
        {
            var estacaoDTO = new EstacaoPostDTO
            {
                Nome = "Estacao Teste",
                Descricao = "Estacao de Teste",
                Inventario = "Inventario de Teste",
                Ordem = 0
            };

            var result = await _controller.AddEstacao(estacaoDTO);

            var _requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(ErrorMessages.OrdemMenorIgualZero, _requestResult.Value);
        }

        [Fact]
        public async Task UpdateEstacao_ErroQuandoOrdemExiste()
        {
            var estacaoDTO_1 = new EstacaoPutDTO
            {
                Nome = "Estacao Teste",
                Descricao = "Estacao de Teste",
                Inventario = "Inventario de Teste",
                Ordem = 2,
                Ativo = true
            };

            var result = await _controller.UpdateEstacaoById(1, estacaoDTO_1);

            var _requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(ErrorMessages.OrdemExiste, _requestResult.Value);
        }

        [Fact]
        public async Task UpdateEstacao_OkComOrdemIgualAtual()
        {
            var estacaoDTO_1 = new EstacaoPutDTO
            {
                Nome = "Estacao Teste",
                Descricao = "Estacao de Teste",
                Inventario = "Inventario de Teste",
                Ordem = 1,
                Ativo = true
            };

            var result = await _controller.UpdateEstacaoById(1, estacaoDTO_1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEstacao_ErroQuandoOrdemMenorIgualZero()
        {
            var estacaoDTO = new EstacaoPutDTO
            {
                Nome = "Estacao Teste",
                Descricao = "Estacao de Teste",
                Inventario = "Inventario de Teste",
                Ordem = 0,
                Ativo = true
            };

            var result = await _controller.UpdateEstacaoById(1, estacaoDTO);

            var _requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(ErrorMessages.OrdemMenorIgualZero, _requestResult.Value);
        }
    }
}