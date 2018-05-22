using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Expshare.Models
{
    public partial class ExpShareContext : DbContext
    {
        public ExpShareContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Grupa> Grupa { get; set; }
        public virtual DbSet<GrupaKorisnik> GrupaKorisnik { get; set; }
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<Lozinka> Lozinka { get; set; }
        public virtual DbSet<TrenutnoStanjeKorisnika> TrenutnoStanjeKorisnika { get; set; }
        public virtual DbSet<TrenutnoStanjeKorisnikaUgrupi> TrenutnoStanjeKorisnikaUgrupi { get; set; }
        public virtual DbSet<Transakcija> Transakcija { get; set; }
        public virtual DbSet<StanjeIzmeduKorisnika> StanjeIzmeduKorisnika { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grupa>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.NazivGrupa)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GrupaKorisnik>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.ToTable("Grupa_Korisnik");

                entity.HasOne(d => d.IdGrupaNavigation)
                    .WithMany(p => p.GrupaKorisnik)
                    .HasForeignKey(d => d.IdGrupa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grupa_Korisnik_Grupa");

                entity.HasOne(d => d.IdKorisnikNavigation)
                    .WithMany(p => p.GrupaKorisnik)
                    .HasForeignKey(d => d.IdKorisnik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grupa_Korisnik_Korisnik");
            });

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.EmailKorisnik)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode(true);
            });

            modelBuilder.Entity<Lozinka>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.LozinkaHash).IsRequired();

                entity.Property(e => e.LozinkaSalt).IsRequired();

                entity.HasOne(d => d.IdLozinkaNavigation)
                    .WithOne(p => p.Lozinka)
                    .HasForeignKey<Lozinka>(d => d.ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lozinka_Korisnik");
            });

            modelBuilder.Entity<TrenutnoStanjeKorisnika>(entity =>
            {
                entity.HasKey(e => e.IdKorisnik);

                entity.ToTable("TrenutnoStanjeKorisnika");

                entity.Property(e => e.IdKorisnik).ValueGeneratedNever();

                entity.Property(e => e.Stanje).HasColumnType("money");

                entity.HasOne(d => d.IdKorisnikNavigation)
                    .WithOne(p => p.TrenutnoStanjeKorisnika)
                    .HasForeignKey<TrenutnoStanjeKorisnika>(d => d.IdKorisnik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrenutnoStanjeKorisnika_Korisnik");

            });

            modelBuilder.Entity<TrenutnoStanjeKorisnikaUgrupi>(entity =>
            {
                entity.HasKey(e => e.IdKorisnik);

                entity.ToTable("TrenutnoStanjeKorisnikaUGrupi");

                entity.Property(e => e.IdKorisnik).ValueGeneratedNever();

                entity.Property(e => e.Stanje).HasColumnType("money");

                entity.HasOne(d => d.IdGrupaNavigation)
                    .WithMany(p => p.TrenutnoStanjeKorisnikaUgrupi)
                    .HasForeignKey(d => d.IdGrupa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrenutnoStanjeKorisnikaUGrupi_Grupa");

                entity.HasOne(d => d.IdKorisnikNavigation)
                    .WithOne(p => p.TrenutnoStanjeKorisnikaUgrupi)
                    .HasForeignKey<TrenutnoStanjeKorisnikaUgrupi>(d => d.IdKorisnik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrenutnoStanjeKorisnikaUGrupi_Korisnik");
            });

            modelBuilder.Entity<Transakcija>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.Iznos).HasColumnType("money");

                entity.Property(e => e.Datum).HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.HasOne(d => d.IdGrupaNavigation)
                    .WithMany(p => p.Uplata)
                    .HasForeignKey(d => d.IdGrupa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transakcija_Grupa");

                entity.HasOne(d => d.IdPlatiteljNavigation)
                    .WithMany(p => p.TransakcijaIdPlatiteljNavigation)
                    .HasForeignKey(d => d.IdPlatitelj)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transakcija_Platitelj_Korisnik");

                entity.HasOne(d => d.IdPrimateljNavigation)
                    .WithMany(p => p.TransakcijaIdPrimateljNavigation)
                    .HasForeignKey(d => d.IdPrimatelj)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transakcija_Primatelj_Korisnik");

            });

            modelBuilder.Entity<StanjeIzmeduKorisnika>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.ToTable("StanjeIzmeduKorisnika");

                entity.Property(e => e.IdKorisnik).ValueGeneratedNever();

                entity.Property(e => e.IdDugovatelj).ValueGeneratedNever();

                entity.Property(e => e.Stanje).HasColumnType("money");

                entity.HasOne(d => d.IdGrupaNavigation)
                    .WithMany(p => p.StanjeIzmeduKorisnika)
                    .HasForeignKey(d => d.IdGrupa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StanjeIzmeduKorisnika_Grupa");

                entity.HasOne(d => d.IdKorisnikNavigation)
                    .WithMany(p => p.StanjeIzmeduKorisnikaKorisnik)
                    .HasForeignKey(d => d.IdKorisnik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StanjeIzmeduKorisnika_Korisnik_Korisnik");

                entity.HasOne(d => d.IdDugovateljNavigation)
                    .WithMany(p => p.StanjeIzmeduKorisnikaDugovatelj)
                    .HasForeignKey(d => d.IdDugovatelj)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StanjeIzmeduKorisnika_Dugovatelj_Korisnik");

            });

        }
    }
}
