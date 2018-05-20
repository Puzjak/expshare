using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Expshare.Migrations
{
    public partial class TrenutnoStanjeKorisnika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrenutnoStanjeKorisnika",
                columns: table => new
                {
                    IdKorisnik = table.Column<Guid>(nullable: false),
                    Stanje = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrenutnoStanjeKorisnika", x => x.IdKorisnik);
                    table.ForeignKey(
                        name: "FK_TrenutnoStanjeKorisnika_Korisnik",
                        column: x => x.IdKorisnik,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrenutnoStanjeKorisnika");
        }
    }
}
