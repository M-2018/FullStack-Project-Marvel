using APIMarvelComics.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMarvelComics.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Favorito>()
                .HasOne(f => f.Usuario)
                .WithMany()
                .HasForeignKey(f => f.UsuarioId);
        }
    }
}
