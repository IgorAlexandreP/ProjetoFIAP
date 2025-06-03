using ProjetoFiap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProjetoFiap.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<UsuarioJogo> UsuarioJogos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
