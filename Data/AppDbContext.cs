using Microsoft.EntityFrameworkCore;
using SistemaCadastroLogin.Models;

namespace SistemaCadastroLogin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Evento> Eventos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura a propriedade 'ValorEvento' para um tipo de coluna específico e precisão
            modelBuilder.Entity<Evento>()
                .Property(e => e.ValorEvento)
                .HasColumnType("decimal(18,2)"); // 18 é a precisão, 2 é a escala (quantidade de casas decimais)
        }
    }
}
