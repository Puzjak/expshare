using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Expshare.Migrations
{
    public partial class NepotrebanPrimateljIdNaUplati : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uplata_Primatelj",
                table: "Transakcija");

            migrationBuilder.DropIndex(
                name: "IX_Uplata_IdPrimatelj",
                table: "Transakcija");

            migrationBuilder.DropColumn(
                name: "IdPrimatelj",
                table: "Transakcija");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdPrimatelj",
                table: "Transakcija",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uplata_IdPrimatelj",
                table: "Transakcija",
                column: "IdPrimatelj");

            migrationBuilder.AddForeignKey(
                name: "FK_Uplata_Primatelj",
                table: "Transakcija",
                column: "IdPrimatelj",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
