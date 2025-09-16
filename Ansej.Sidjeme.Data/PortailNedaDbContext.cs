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
            modelBuilder.Entity<PreInscriptionSousTraitence>().HasOne(x => x.CommuneNaissance)
                          .WithMany(x => x.InscriptionsCNais)
                          .HasForeignKey(x => x.CommuneNaissanceId)
                          .OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<DeclarationHonneurMI>()
            .HasIndex(d => d.ProjectNumber)
            .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ContactAgenceWilaya> ContactAgenceWilayas { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<StructureManager> StructureManager { get; set; }
        public DbSet<PreInscriptionSousTraitence> PreInscriptionSousTraitence { get; set; }
        public DbSet<Activite> Activites { get; set; }
        public DbSet<PerimetreActivite> PerimetreActivites { get; set; }
        public DbSet<Wilaya> Wilaya { get; set; }
        public DbSet<Commune> Communes { get; set; }
        public DbSet<DeclarationHonneurMI> DeclarationsHonneurMI { get; set; }
		public DbSet<SecteurActivite> SecteurActivites { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<FormeJuridique> FormesJuridique { get; set; }
		public DbSet<TypeOfNeed> TypesOfNeed { get; set; }
        public DbSet<MotifTraitement> MotifsTraitement { get; set; }

    }
}
