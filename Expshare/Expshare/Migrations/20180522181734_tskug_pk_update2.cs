using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Expshare.Migrations
{
    public partial class tskug_pk_update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrenutnoStanjeKorisnikaUGrupi",
                table: "TrenutnoStanjeKorisnikaUGrupi");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TrenutnoStanjeKorisnikaUGrupi",
                nullable: false,
                defaultValueSql: "newid()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrenutnoStanjeKorisnikaUGrupi",
                table: "TrenutnoStanjeKorisnikaUGrupi",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrenutnoStanjeKorisnikaUGrupi",
                table: "TrenutnoStanjeKorisnikaUGrupi");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TrenutnoStanjeKorisnikaUGrupi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrenutnoStanjeKorisnikaUGrupi",
                table: "TrenutnoStanjeKorisnikaUGrupi",
                columns: new[] { "IdKorisnik", "IdGrupa" });
        }
    }
}
