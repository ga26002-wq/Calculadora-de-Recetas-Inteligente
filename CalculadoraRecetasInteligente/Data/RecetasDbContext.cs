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
    }
}