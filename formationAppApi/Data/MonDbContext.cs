using Microsoft.EntityFrameworkCore;
using formationAppApi.Models;
using Microsoft.EntityFrameworkCore.Metadata;

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


        //Pour mettre le nom de table et de champs en minuscules
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Forcer les noms de table en minuscules ---
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Nom de table
                var tableName = entity.GetTableName();
                if (tableName != null)
                {
                    entity.SetTableName(tableName.ToLower());
                }

                // --- Forcer les noms de colonnes en minuscules ---
                foreach (var property in entity.GetProperties())
                {
                    var columnName = property.GetColumnName(StoreObjectIdentifier.Table(tableName!, null));
                    if (columnName != null)
                    {
                        property.SetColumnName(columnName.ToLower());
                    }
                }
            }
        }

    }
}