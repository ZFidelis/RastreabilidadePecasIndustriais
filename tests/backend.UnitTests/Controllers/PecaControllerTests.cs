using backend.Controller;
using backend.Data;
using backend.Model;
using backend.Model.DTO.Peca;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.UnitTests.Controllers
{

    public class PecaControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid().ToString()).Options;
            var dbContext = new AppDbContext(options);
            return dbContext;
        }

        [Fact]
        public async Task AddPeca_Status0_QuandoEstacaoOriomNull()
        {
            var dbContext = GetInMemoryDbContext();
            var controller = new PecaController(dbContext);
            var pecaDTO = new PecaPostDTO
            {
                Partnumber = "PnTeste",
                Descricao = "Peça de Teste"
            };

            var result = await controller.AddPeca(pecaDTO);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var peca = Assert.IsType<Peca>(createdResult.Value);

            Assert.Equal(StatusPeca.Pendente, peca.Status);
        }

    }
}