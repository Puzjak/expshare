﻿// <auto-generated />
using Expshare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Expshare.Migrations
{
    [DbContext(typeof(ExpShareContext))]
    [Migration("20180519131308_DodanoStanjeIzmeduKorisnika")]
    partial class DodanoStanjeIzmeduKorisnika
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Expshare.Models.Dugovanje", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdDugovatelj");

                    b.Property<Guid>("IdUplata");

                    b.Property<decimal>("Iznos")
                        .HasColumnType("money");

                    b.HasKey("ID");

                    b.HasIndex("IdDugovatelj");

                    b.HasIndex("IdUplata");

                    b.ToTable("Dugovanje");
                });

            modelBuilder.Entity("Expshare.Models.Grupa", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NazivGrupa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("Grupa");
                });

            modelBuilder.Entity("Expshare.Models.GrupaKorisnik", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdGrupa");

                    b.Property<Guid>("IdKorisnik");

                    b.HasKey("ID");

                    b.HasIndex("IdGrupa");

                    b.HasIndex("IdKorisnik");

                    b.ToTable("Grupa_Korisnik");
                });

            modelBuilder.Entity("Expshare.Models.Korisnik", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailKorisnik")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("Expshare.Models.Lozinka", b =>
                {
                    b.Property<Guid>("ID");

                    b.Property<string>("LozinkaHash")
                        .IsRequired();

                    b.Property<string>("LozinkaSalt")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Lozinka");
                });

            modelBuilder.Entity("Expshare.Models.StanjeIzmeduKorisnika", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdDugovatelj");

                    b.Property<Guid>("IdGrupa");

                    b.Property<Guid>("IdKorisnik");

                    b.Property<decimal>("Stanje")
                        .HasColumnType("money");

                    b.HasKey("ID");

                    b.HasIndex("IdDugovatelj");

                    b.HasIndex("IdGrupa");

                    b.HasIndex("IdKorisnik");

                    b.ToTable("StanjeIzmeduKorisnika");
                });

            modelBuilder.Entity("Expshare.Models.TrenutnoStanjeKorisnika", b =>
                {
                    b.Property<Guid>("IdKorisnik");

                    b.Property<decimal>("Stanje")
                        .HasColumnType("money");

                    b.HasKey("IdKorisnik");

                    b.ToTable("TrenutnoStanjeKorisnika");
                });

            modelBuilder.Entity("Expshare.Models.TrenutnoStanjeKorisnikaUgrupi", b =>
                {
                    b.Property<Guid>("IdKorisnik");

                    b.Property<Guid>("IdGrupa");

                    b.Property<decimal>("Stanje")
                        .HasColumnType("money");

                    b.HasKey("IdKorisnik");

                    b.HasIndex("IdGrupa");

                    b.ToTable("TrenutnoStanjeKorisnikaUGrupi");
                });

            modelBuilder.Entity("Expshare.Models.Transakcija", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("IdGrupa");

                    b.Property<Guid>("IdKorisnik");

                    b.Property<decimal>("Iznos")
                        .HasColumnType("money");

                    b.HasKey("ID");

                    b.HasIndex("IdGrupa");

                    b.HasIndex("IdKorisnik");

                    b.ToTable("Transakcija");
                });

            modelBuilder.Entity("Expshare.Models.Dugovanje", b =>
                {
                    b.HasOne("Expshare.Models.Korisnik", "IdDugovateljNavigation")
                        .WithMany("Dugovanje")
                        .HasForeignKey("IdDugovatelj")
                        .HasConstraintName("FK_Dugovanje_Dugovatelj");

                    b.HasOne("Expshare.Models.Transakcija", "IdUplataNavigation")
                        .WithMany("Dugovanje")
                        .HasForeignKey("IdUplata")
                        .HasConstraintName("FK_Dugovanje_Uplata");
                });

            modelBuilder.Entity("Expshare.Models.GrupaKorisnik", b =>
                {
                    b.HasOne("Expshare.Models.Grupa", "IdGrupaNavigation")
                        .WithMany("GrupaKorisnik")
                        .HasForeignKey("IdGrupa")
                        .HasConstraintName("FK_Grupa_Korisnik_Grupa");

                    b.HasOne("Expshare.Models.Korisnik", "IdKorisnikNavigation")
                        .WithMany("GrupaKorisnik")
                        .HasForeignKey("IdKorisnik")
                        .HasConstraintName("FK_Grupa_Korisnik_Korisnik");
                });

            modelBuilder.Entity("Expshare.Models.Lozinka", b =>
                {
                    b.HasOne("Expshare.Models.Korisnik", "IdLozinkaNavigation")
                        .WithOne("Lozinka")
                        .HasForeignKey("Expshare.Models.Lozinka", "ID")
                        .HasConstraintName("FK_Lozinka_Korisnik");
                });

            modelBuilder.Entity("Expshare.Models.StanjeIzmeduKorisnika", b =>
                {
                    b.HasOne("Expshare.Models.Korisnik", "IdDugovateljNavigation")
                        .WithMany("StanjeIzmeduKorisnikaDugovatelj")
                        .HasForeignKey("IdDugovatelj")
                        .HasConstraintName("FK_StanjeIzmeduKorisnika_Dugovatelj_Korisnik");

                    b.HasOne("Expshare.Models.Grupa", "IdGrupaNavigation")
                        .WithMany("StanjeIzmeduKorisnika")
                        .HasForeignKey("IdGrupa")
                        .HasConstraintName("FK_StanjeIzmeduKorisnika_Grupa");

                    b.HasOne("Expshare.Models.Korisnik", "IdKorisnikNavigation")
                        .WithMany("StanjeIzmeduKorisnikaKorisnik")
                        .HasForeignKey("IdKorisnik")
                        .HasConstraintName("FK_StanjeIzmeduKorisnika_Korisnik_Korisnik");
                });

            modelBuilder.Entity("Expshare.Models.TrenutnoStanjeKorisnika", b =>
                {
                    b.HasOne("Expshare.Models.Korisnik", "IdKorisnikNavigation")
                        .WithOne("TrenutnoStanjeKorisnika")
                        .HasForeignKey("Expshare.Models.TrenutnoStanjeKorisnika", "IdKorisnik")
                        .HasConstraintName("FK_TrenutnoStanjeKorisnika_Korisnik");
                });

            modelBuilder.Entity("Expshare.Models.TrenutnoStanjeKorisnikaUgrupi", b =>
                {
                    b.HasOne("Expshare.Models.Grupa", "IdGrupaNavigation")
                        .WithMany("TrenutnoStanjeKorisnikaUgrupi")
                        .HasForeignKey("IdGrupa")
                        .HasConstraintName("FK_TrenutnoStanjeKorisnikaUGrupi_Grupa");

                    b.HasOne("Expshare.Models.Korisnik", "IdKorisnikNavigation")
                        .WithOne("TrenutnoStanjeKorisnikaUgrupi")
                        .HasForeignKey("Expshare.Models.TrenutnoStanjeKorisnikaUgrupi", "IdKorisnik")
                        .HasConstraintName("FK_TrenutnoStanjeKorisnikaUGrupi_Korisnik");
                });

            modelBuilder.Entity("Expshare.Models.Transakcija", b =>
                {
                    b.HasOne("Expshare.Models.Grupa", "IdGrupaNavigation")
                        .WithMany("Transakcija")
                        .HasForeignKey("IdGrupa")
                        .HasConstraintName("FK_Uplata_Grupa");

                    b.HasOne("Expshare.Models.Korisnik", "IdKorisnikNavigation")
                        .WithMany("UplataIdKorisnikNavigation")
                        .HasForeignKey("IdKorisnik")
                        .HasConstraintName("FK_Uplata_Korisnik");
                });
#pragma warning restore 612, 618
        }
    }
}
