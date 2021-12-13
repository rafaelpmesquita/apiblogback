using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
          public ApplicationDbContext(DbContextOptions options) :
            base(options) { }
        public DbSet<Especialidades> Especialidades { get; set; }
        public DbSet<NavBar> NavBar { get; set; }
        public DbSet<TimeLine> TimeLine { get; set; }
        public DbSet<Usuario> Usuario { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Especialidades>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<NavBar>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<TimeLine>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Usuario>()
                .HasKey(p => p.Id);
        }
    }
}