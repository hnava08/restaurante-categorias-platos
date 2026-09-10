using Microsoft.EntityFrameworkCore;
using RestauranteCategoriasPlatos.Models;

namespace RestauranteCategoriasPlatos.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Plato> Platos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación uno a muchos: Categoría -> Platos
        modelBuilder.Entity<Plato>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Platos)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
