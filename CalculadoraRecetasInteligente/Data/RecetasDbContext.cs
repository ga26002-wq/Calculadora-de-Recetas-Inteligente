using CalculadoraRecetasInteligente.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraRecetasInteligente.Data
{
    public class RecetasDbContext : DbContext
    {
        public RecetasDbContext(DbContextOptions<RecetasDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<Receta> Recetas { get; set; }

        public DbSet<Ingrediente> Ingredientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CONFIGURACIÓN DE RECETA
            // Evita el error del OUTPUT cuando la tabla tiene triggers
            modelBuilder.Entity<Receta>()
                .ToTable("Recetas", "recetas", tb =>
                {
                    tb.UseSqlOutputClause(false);
                });

            // CONFIGURACIÓN DE INGREDIENTE
            modelBuilder.Entity<Ingrediente>()
                .Property(i => i.Cantidad)
                .HasPrecision(10, 2);

            // RELACIÓN RECETA - INGREDIENTES
            modelBuilder.Entity<Ingrediente>()
                .HasOne(i => i.Receta)
                .WithMany(r => r.Ingredientes)
                .HasForeignKey(i => i.RecetaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}