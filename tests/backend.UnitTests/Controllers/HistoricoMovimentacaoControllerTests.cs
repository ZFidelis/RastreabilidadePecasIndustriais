using backend.Controller;
using backend.Data;
using backend.Model;
using backend.Model.DTO.HistoricoMovimentacao;
using backend.Shared.Constants;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.UnitTests.Controllers
{
    public class HistoricoMovimentacaoControllerTests
    {
        private readonly AppDbContext _dbContext;
        private readonly HistoricoMovimentacaoController _controller;

        public HistoricoMovimentacaoControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid().ToString()).Options;
            _dbContext = new AppDbContext(options);
            _controller = new HistoricoMovimentacaoController(_dbContext);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var estacaoOrigem = new Estacao
            {
                Id = 1,
                Inventario = "InventarioUnicoOrigem",
                Nome = "Estacao Origem Teste",
                Descricao = "Estacao para Testes Unitarios",
                Ativo = true,
                DataCriacao = DateTime.Now,
                DataAtualizacao = null,
                Ordem = 1
            };
            var estacaoIntermediaria = new Estacao
            {
                Id = 2,
                Inventario = "InventarioUnicoIntermediario",
                Nome = "Estacao Intermediario Teste",
                Descricao = "Estacao para Testes Unitarios",
                Ativo = true,
                DataCriacao = DateTime.Now,
                DataAtualizacao = null,
                Ordem = 2
            };
            var estacaoDestino = new Estacao
            {
                Id = 3,
                Inventario = "InventarioUnicoDestino",
                Nome = "Estacao Destino Teste",
                Descricao = "Estacao para Testes Unitarios",
                Ativo = true,
                DataCriacao = DateTime.Now,
                DataAtualizacao = null,
                Ordem = 3
            };

            var pecaPendente = new Peca
            {
                Id = 1,
                Partnumber = "Peca Pendente Teste",
                Descricao = "Peca para Testes Unitarios",
                EstacaoAtual = null,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Status = StatusPeca.Pendente
            };
            var pecaEmProcesso = new Peca
            {
                Id = 2,
                Partnumber = "Peca Em Processo Teste",
                Descricao = "Peca para Testes Unitarios",
                EstacaoAtual = estacaoIntermediaria,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Status = StatusPeca.EmProcesso
            };
            var pecaFinalizada = new Peca
            {
                Id = 3,
                Partnumber = "Peca Finalizada Teste",
                Descricao = "Peca para Testes Unitarios",
                EstacaoAtual = estacaoDestino,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Status = StatusPeca.Finalizada
            };

            _dbContext.tb_estacao.Add(estacaoOrigem);
            _dbContext.tb_estacao.Add(estacaoIntermediaria);
            _dbContext.tb_estacao.Add(estacaoDestino);
            _dbContext.tb_peca.Add(pecaPendente);
            _dbContext.tb_peca.Add(pecaEmProcesso);
            _dbContext.tb_peca.Add(pecaFinalizada);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task MovimentarPeca_DeveDefinirStatusEmProgresso_QuandoNaoForUltimaEstacao()
        {

            var pnPeca = "Peca Pendente Teste";
            var movimento = new MovimentarDTO
            {
                Partnumber = pnPeca,
                EstacaoDestinoId = 1,
                Responsavel = "Usuario de Testes",
                Observacao = "Observacao Teste"
            };

            var result = await _controller.MovimentarDTO(movimento);

            var pecaMovimentada = await _dbContext.tb_peca.FirstAsync(p => p.Partnumber == pnPeca);

            Assert.Equal(StatusPeca.EmProcesso, pecaMovimentada.Status);
        }

        [Fact]
        public async Task MovimentarPeca_DeveDefinirStatusFinalizado_QuandoForUltimaEstacao()
        {
            var pnPeca = "Peca Em Processo Teste";
            var movimento = new MovimentarDTO
            {
                Partnumber = pnPeca,
                EstacaoDestinoId = 3,
                Responsavel = "Usuario de Testes",
                Observacao = "Observacao Teste"
            };

            var result = await _controller.MovimentarDTO(movimento);

            var pecaMovimentada = await _dbContext.tb_peca.FirstAsync(p => p.Partnumber == pnPeca);

            Assert.Equal(StatusPeca.Finalizada, pecaMovimentada.Status);
        }

        [Fact]
        public async Task MovimentarPeca_NaoPodePularEstacoes()
        {
            var pnPeca = "Peca Pendente Teste";
            var movimento = new MovimentarDTO
            {
                Partnumber = pnPeca,
                EstacaoDestinoId = 3,
                Responsavel = "Usuario de Testes",
                Observacao = "Observacao Teste"
            };

            var result = await _controller.MovimentarDTO(movimento);

            var pecaMovimentada = await _dbContext.tb_peca.FirstAsync(p => p.Partnumber == pnPeca);

            var _requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(ErrorMessages.PularEstacao, _requestResult.Value);
        }

        [Fact]
        public async Task MovimentarPeca_NaoPodeRetrocederEstacoes()
        {
            var pnPeca = "Peca Em Processo Teste";
            var movimento = new MovimentarDTO
            {
                Partnumber = pnPeca,
                EstacaoDestinoId = 1,
                Responsavel = "Usuario de Testes",
                Observacao = "Observacao Teste"
            };

            var result = await _controller.MovimentarDTO(movimento);

            var pecaMovimentada = await _dbContext.tb_peca.FirstAsync(p => p.Partnumber == pnPeca);

            var _requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(ErrorMessages.RetrocederEstacao, _requestResult.Value);
        }

        [Fact]
        public async Task MovimentarPeca_NaoPodeContinuarNaEstacao()
        {
            var pnPeca = "Peca Em Processo Teste";
            var movimento = new MovimentarDTO
            {
                Partnumber = pnPeca,
                EstacaoDestinoId = 2,
                Responsavel = "Usuario de Testes",
                Observacao = "Observacao Teste"
            };

            var result = await _controller.MovimentarDTO(movimento);

            var pecaMovimentada = await _dbContext.tb_peca.FirstAsync(p => p.Partnumber == pnPeca);

            var _requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(ErrorMessages.MesmaEstacao, _requestResult.Value);
        }

        [Fact]
        public async Task MovimentarPeca_NaoPodeMovimentarPecasFinalizadas()
        {
            var pnPeca = "Peca Finalizada Teste";
            var movimento = new MovimentarDTO
            {
                Partnumber = pnPeca,
                EstacaoDestinoId = 2,
                Responsavel = "Usuario de Testes",
                Observacao = "Observacao Teste"
            };

            var result = await _controller.MovimentarDTO(movimento);

            var pecaMovimentada = await _dbContext.tb_peca.FirstAsync(p => p.Partnumber == pnPeca);

            var _requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(ErrorMessages.PecaFinalizada, _requestResult.Value);
        }
    }
}