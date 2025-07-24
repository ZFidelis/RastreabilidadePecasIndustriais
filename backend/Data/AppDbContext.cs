using Microsoft.EntityFrameworkCore;
using backend.Model;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Peca> tb_peca { get; set; }
        public DbSet<Estacao> tb_estacao { get; set; }
    }
}