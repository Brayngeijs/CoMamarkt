using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoMamarkt.Data.Migrations
{
    public partial class products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Bestelling_BestellingId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Bestelling");

            migrationBuilder.DropIndex(
                name: "IX_Product_BestellingId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "BestellingId",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BestellingId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Bestelling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Achternaam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BestellingDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Huisnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Straat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tussenvoegsel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Woonplaats = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestelling", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_BestellingId",
                table: "Product",
                column: "BestellingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Bestelling_BestellingId",
                table: "Product",
                column: "BestellingId",
                principalTable: "Bestelling",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
