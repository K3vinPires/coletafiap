using ColetaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace ColetaApi.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<ColetaModel> Coleta { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColetaModel>( e =>
            {
                e.ToTable("COLETA");

                e.HasKey(e => e.Pk_id);

                e.Property(e => e.Ds_coleta).HasMaxLength(50);
                e.Property(e => e.Ds_tipocoleta).HasMaxLength(50);
                e.Property(e => e.Dt_coleta);

                e.HasIndex(e => e.Ds_coleta);

            });
        }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }
    }
}
