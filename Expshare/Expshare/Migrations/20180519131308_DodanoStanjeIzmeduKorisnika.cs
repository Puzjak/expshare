using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Expshare.Migrations
{
    public partial class DodanoStanjeIzmeduKorisnika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StanjeIzmeduKorisnika",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IdDugovatelj = table.Column<Guid>(nullable: false),
                    IdGrupa = table.Column<Guid>(nullable: false),
                    IdKorisnik = table.Column<Guid>(nullable: false),
                    Stanje = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanjeIzmeduKorisnika", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StanjeIzmeduKorisnika_Dugovatelj_Korisnik",
                        column: x => x.IdDugovatelj,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StanjeIzmeduKorisnika_Grupa",
                        column: x => x.IdGrupa,
                        principalTable: "Grupa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StanjeIzmeduKorisnika_Korisnik_Korisnik",
                        column: x => x.IdKorisnik,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StanjeIzmeduKorisnika_IdDugovatelj",
                table: "StanjeIzmeduKorisnika",
                column: "IdDugovatelj");

            migrationBuilder.CreateIndex(
                name: "IX_StanjeIzmeduKorisnika_IdGrupa",
                table: "StanjeIzmeduKorisnika",
                column: "IdGrupa");

            migrationBuilder.CreateIndex(
                name: "IX_StanjeIzmeduKorisnika_IdKorisnik",
                table: "StanjeIzmeduKorisnika",
                column: "IdKorisnik");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StanjeIzmeduKorisnika");
        }
    }
}
