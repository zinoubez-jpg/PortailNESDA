using Ansej.Sidjeme.Domain;
using Ansej.Sidjeme.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ansej.Sidjeme.Data
{
    public class PortailNedaDbContext : DbContext
    {
        public PortailNedaDbContext() : base()
        {

        }

        public PortailNedaDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=.\sqlexpress;Database=SidjemeDB;Integrated Security=true;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Wilaya>().
                 HasMany(w => w.ContactUs)
                .WithOne(c => c.Wilaya)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StructureManager>()
                .HasMany(a => a.Consultations)
                .WithOne(c => c.StructureManager)
                .HasForeignKey(c => c.StructureManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StructureManager>()
            .HasOne(c => c.ParentStructureManager)
            .WithMany(c => c.SubStructures)
            .HasForeignKey(c => c.ParentStructureManagerId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Article>()
            .HasIndex(a => a.Slug)
            .IsUnique();


            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ContactAgenceWilaya> ContactAgenceWilayas { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<StructureManager> StructureManager { get; set; }

    }
}
