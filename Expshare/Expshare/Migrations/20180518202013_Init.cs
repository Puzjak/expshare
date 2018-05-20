using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Expshare.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupa",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    NazivGrupa = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupa", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    EmailKorisnik = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VrstaUplate",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaUplate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Grupa_Korisnik",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IdGrupa = table.Column<Guid>(nullable: false),
                    IdKorisnik = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupa_Korisnik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Grupa_Korisnik_Grupa",
                        column: x => x.IdGrupa,
                        principalTable: "Grupa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grupa_Korisnik_Korisnik",
                        column: x => x.IdKorisnik,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lozinka",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    LozinkaHash = table.Column<string>(nullable: false),
                    LozinkaSalt = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lozinka", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lozinka_Korisnik",
                        column: x => x.ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrenutnoStanjeKorisnikaUGrupi",
                columns: table => new
                {
                    IdKorisnik = table.Column<Guid>(nullable: false),
                    IdGrupa = table.Column<Guid>(nullable: false),
                    Stanje = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrenutnoStanjeKorisnikaUGrupi", x => x.IdKorisnik);
                    table.ForeignKey(
                        name: "FK_TrenutnoStanjeKorisnikaUGrupi_Grupa",
                        column: x => x.IdGrupa,
                        principalTable: "Grupa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrenutnoStanjeKorisnikaUGrupi_Korisnik",
                        column: x => x.IdKorisnik,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transakcija",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IdGrupa = table.Column<Guid>(nullable: false),
                    IdKorisnik = table.Column<Guid>(nullable: false),
                    IdPrimatelj = table.Column<Guid>(nullable: true),
                    IdVrstaUplate = table.Column<Guid>(nullable: false),
                    Iznos = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplata", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Uplata_Grupa",
                        column: x => x.IdGrupa,
                        principalTable: "Grupa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uplata_Korisnik",
                        column: x => x.IdKorisnik,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uplata_Primatelj",
                        column: x => x.IdPrimatelj,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uplata_VrstaUplate",
                        column: x => x.IdVrstaUplate,
                        principalTable: "VrstaUplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dugovanje",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IdDugovatelj = table.Column<Guid>(nullable: false),
                    IdGrupa = table.Column<Guid>(nullable: false),
                    IdUplata = table.Column<Guid>(nullable: false),
                    Iznos = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dugovanje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Dugovanje_Dugovatelj",
                        column: x => x.IdDugovatelj,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dugovanje_Grupa",
                        column: x => x.IdGrupa,
                        principalTable: "Grupa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dugovanje_Uplata",
                        column: x => x.IdUplata,
                        principalTable: "Transakcija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dugovanje_IdDugovatelj",
                table: "Dugovanje",
                column: "IdDugovatelj");

            migrationBuilder.CreateIndex(
                name: "IX_Dugovanje_IdGrupa",
                table: "Dugovanje",
                column: "IdGrupa");

            migrationBuilder.CreateIndex(
                name: "IX_Dugovanje_IdUplata",
                table: "Dugovanje",
                column: "IdUplata");

            migrationBuilder.CreateIndex(
                name: "IX_Grupa_Korisnik_IdGrupa",
                table: "Grupa_Korisnik",
                column: "IdGrupa");

            migrationBuilder.CreateIndex(
                name: "IX_Grupa_Korisnik_IdKorisnik",
                table: "Grupa_Korisnik",
                column: "IdKorisnik");

            migrationBuilder.CreateIndex(
                name: "IX_TrenutnoStanjeKorisnikaUGrupi_IdGrupa",
                table: "TrenutnoStanjeKorisnikaUGrupi",
                column: "IdGrupa");

            migrationBuilder.CreateIndex(
                name: "IX_Uplata_IdGrupa",
                table: "Transakcija",
                column: "IdGrupa");

            migrationBuilder.CreateIndex(
                name: "IX_Uplata_IdKorisnik",
                table: "Transakcija",
                column: "IdKorisnik");

            migrationBuilder.CreateIndex(
                name: "IX_Uplata_IdPrimatelj",
                table: "Transakcija",
                column: "IdPrimatelj");

            migrationBuilder.CreateIndex(
                name: "IX_Uplata_IdVrstaUplate",
                table: "Transakcija",
                column: "IdVrstaUplate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dugovanje");

            migrationBuilder.DropTable(
                name: "Grupa_Korisnik");

            migrationBuilder.DropTable(
                name: "Lozinka");

            migrationBuilder.DropTable(
                name: "TrenutnoStanjeKorisnikaUGrupi");

            migrationBuilder.DropTable(
                name: "Transakcija");

            migrationBuilder.DropTable(
                name: "Grupa");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "VrstaUplate");
        }
    }
}
