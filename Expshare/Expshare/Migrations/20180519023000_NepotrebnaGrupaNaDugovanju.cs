using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Expshare.Migrations
{
    public partial class NepotrebnaGrupaNaDugovanju : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dugovanje_Grupa",
                table: "Dugovanje");

            migrationBuilder.DropIndex(
                name: "IX_Dugovanje_IdGrupa",
                table: "Dugovanje");

            migrationBuilder.DropColumn(
                name: "IdGrupa",
                table: "Dugovanje");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdGrupa",
                table: "Dugovanje",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Dugovanje_IdGrupa",
                table: "Dugovanje",
                column: "IdGrupa");

            migrationBuilder.AddForeignKey(
                name: "FK_Dugovanje_Grupa",
                table: "Dugovanje",
                column: "IdGrupa",
                principalTable: "Grupa",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
