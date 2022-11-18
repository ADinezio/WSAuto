using DiNezioCamiletti.Models;
using Microsoft.EntityFrameworkCore;

namespace DiNezioCamiletti.Data
{
    public class DBAutoContext:DbContext
    {
        public DBAutoContext(DbContextOptions<DBAutoContext> options):base(options){ }

        public DbSet<Auto> Autos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Auto>().ToTable("Vehiculo");
            modelBuilder.Entity<Auto>().HasKey(t => t.AutoId);

            modelBuilder.Entity<Auto>().Property(t => t.Marca).HasColumnType("varchar(50)").IsRequired();
            modelBuilder.Entity<Auto>().Property(t => t.Modelo).HasColumnType("varchar(50)").IsRequired();
            modelBuilder.Entity<Auto>().Property(t => t.Precio).HasColumnType("money");

        }
    }
}
