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
    [Migration("20180518202013_Init")]
    partial class Init
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

                    b.Property<Guid>("IdGrupa");

                    b.Property<Guid>("IdUplata");

                    b.Property<decimal>("Iznos")
                        .HasColumnType("money");

                    b.HasKey("ID");

                    b.HasIndex("IdDugovatelj");

                    b.HasIndex("IdGrupa");

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

                    b.Property<Guid>("IdGrupa");

                    b.Property<Guid>("IdKorisnik");

                    b.Property<Guid?>("IdPrimatelj");

                    b.Property<Guid>("IdVrstaUplate");

                    b.Property<decimal>("Iznos")
                        .HasColumnType("money");

                    b.HasKey("ID");

                    b.HasIndex("IdGrupa");

                    b.HasIndex("IdKorisnik");

                    b.HasIndex("IdPrimatelj");

                    b.HasIndex("IdVrstaUplate");

                    b.ToTable("Transakcija");
                });

            modelBuilder.Entity("Expshare.Models.VrstaUplate", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv")
                        .HasMaxLength(255);

                    b.HasKey("ID");

                    b.ToTable("VrstaUplate");
                });

            modelBuilder.Entity("Expshare.Models.Dugovanje", b =>
                {
                    b.HasOne("Expshare.Models.Korisnik", "IdDugovateljNavigation")
                        .WithMany("Dugovanje")
                        .HasForeignKey("IdDugovatelj")
                        .HasConstraintName("FK_Dugovanje_Dugovatelj");

                    b.HasOne("Expshare.Models.Grupa", "IdGrupaNavigation")
                        .WithMany("Dugovanje")
                        .HasForeignKey("IdGrupa")
                        .HasConstraintName("FK_Dugovanje_Grupa");

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

                    b.HasOne("Expshare.Models.Korisnik", "IdPrimateljNavigation")
                        .WithMany("UplataIdPrimateljNavigation")
                        .HasForeignKey("IdPrimatelj")
                        .HasConstraintName("FK_Uplata_Primatelj");

                    b.HasOne("Expshare.Models.VrstaUplate", "IdVrstaUplateNavigation")
                        .WithMany("Transakcija")
                        .HasForeignKey("IdVrstaUplate")
                        .HasConstraintName("FK_Uplata_VrstaUplate");
                });
#pragma warning restore 612, 618
        }
    }
}
