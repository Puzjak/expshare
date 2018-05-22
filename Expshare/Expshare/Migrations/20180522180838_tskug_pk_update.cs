using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Expshare.Migrations
{
    public partial class tskug_pk_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrenutnoStanjeKorisnikaUGrupi",
                table: "TrenutnoStanjeKorisnikaUGrupi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrenutnoStanjeKorisnikaUGrupi",
                table: "TrenutnoStanjeKorisnikaUGrupi",
                columns: new[] { "IdKorisnik", "IdGrupa" });

            migrationBuilder.CreateIndex(
                name: "IX_TrenutnoStanjeKorisnikaUGrupi_IdKorisnik",
                table: "TrenutnoStanjeKorisnikaUGrupi",
                column: "IdKorisnik",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrenutnoStanjeKorisnikaUGrupi",
                table: "TrenutnoStanjeKorisnikaUGrupi");

            migrationBuilder.DropIndex(
                name: "IX_TrenutnoStanjeKorisnikaUGrupi_IdKorisnik",
                table: "TrenutnoStanjeKorisnikaUGrupi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrenutnoStanjeKorisnikaUGrupi",
                table: "TrenutnoStanjeKorisnikaUGrupi",
                column: "IdKorisnik");
        }
    }
}
