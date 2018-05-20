using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Expshare.Migrations
{
    public partial class VrstaUplateNijePotrebna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uplata_VrstaUplate",
                table: "Transakcija");

            migrationBuilder.DropTable(
                name: "VrstaUplate");

            migrationBuilder.DropIndex(
                name: "IX_Uplata_IdVrstaUplate",
                table: "Transakcija");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Uplata_IdVrstaUplate",
                table: "Transakcija",
                column: "IdVrstaUplate");

            migrationBuilder.AddForeignKey(
                name: "FK_Uplata_VrstaUplate",
                table: "Transakcija",
                column: "IdVrstaUplate",
                principalTable: "VrstaUplate",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
