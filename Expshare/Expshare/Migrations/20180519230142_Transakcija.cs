using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Expshare.Migrations
{
    public partial class Transakcija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uplata_Grupa",
                table: "Transakcija");

            migrationBuilder.DropForeignKey(
                name: "FK_Uplata_Korisnik",
                table: "Transakcija");

            migrationBuilder.DropTable(
                name: "Dugovanje");

            migrationBuilder.RenameColumn(
                name: "IdKorisnik",
                table: "Transakcija",
                newName: "IdPrimatelj");

            migrationBuilder.RenameIndex(
                name: "IX_Transakcija_IdKorisnik",
                table: "Transakcija",
                newName: "IX_Transakcija_IdPrimatelj");

            migrationBuilder.AddColumn<Guid>(
                name: "IdPlatitelj",
                table: "Transakcija",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transakcija_IdPlatitelj",
                table: "Transakcija",
                column: "IdPlatitelj");

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcija_Grupa",
                table: "Transakcija",
                column: "IdGrupa",
                principalTable: "Grupa",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcija_Platitelj_Korisnik",
                table: "Transakcija",
                column: "IdPlatitelj",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcija_Primatelj_Korisnik",
                table: "Transakcija",
                column: "IdPrimatelj",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transakcija_Grupa",
                table: "Transakcija");

            migrationBuilder.DropForeignKey(
                name: "FK_Transakcija_Platitelj_Korisnik",
                table: "Transakcija");

            migrationBuilder.DropForeignKey(
                name: "FK_Transakcija_Primatelj_Korisnik",
                table: "Transakcija");

            migrationBuilder.DropIndex(
                name: "IX_Transakcija_IdPlatitelj",
                table: "Transakcija");

            migrationBuilder.DropColumn(
                name: "IdPlatitelj",
                table: "Transakcija");

            migrationBuilder.RenameColumn(
                name: "IdPrimatelj",
                table: "Transakcija",
                newName: "IdKorisnik");

            migrationBuilder.RenameIndex(
                name: "IX_Transakcija_IdPrimatelj",
                table: "Transakcija",
                newName: "IX_Transakcija_IdKorisnik");

            migrationBuilder.CreateTable(
                name: "Dugovanje",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IdDugovatelj = table.Column<Guid>(nullable: false),
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
                name: "IX_Dugovanje_IdUplata",
                table: "Dugovanje",
                column: "IdUplata");

            migrationBuilder.AddForeignKey(
                name: "FK_Uplata_Grupa",
                table: "Transakcija",
                column: "IdGrupa",
                principalTable: "Grupa",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Uplata_Korisnik",
                table: "Transakcija",
                column: "IdKorisnik",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
