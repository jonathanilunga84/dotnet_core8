using Microsoft.EntityFrameworkCore;
using formationAppApi.Models;

namespace formationAppApi.Data
{
    public class MonDbContext : DbContext
    {
        public MonDbContext(DbContextOptions<MonDbContext> options) : base(options)
        {
        }

        public DbSet<Apprenant> Apprenants { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<Note> Notes { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     // Configuration des relations si nécessaire
        //     modelBuilder.Entity<Note>()
        //         .HasOne(n => n.Apprenant)
        //         .WithMany()
        //         .HasForeignKey(n => n.ApprenantId);
        // }
    }
}