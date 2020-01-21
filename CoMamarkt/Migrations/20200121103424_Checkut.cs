using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoMamarkt.Migrations
{
    public partial class Checkut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bestellings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true),
                    Woonplaats = table.Column<string>(nullable: true),
                    Straat = table.Column<string>(nullable: true),
                    Huisnummer = table.Column<string>(nullable: true),
                    BestellingDatum = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestellings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bestellings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BestellingProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestellingId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestellingProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BestellingProducts_Bestellings_BestellingId",
                        column: x => x.BestellingId,
                        principalTable: "Bestellings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestellingProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestellingProducts_BestellingId",
                table: "BestellingProducts",
                column: "BestellingId");

            migrationBuilder.CreateIndex(
                name: "IX_BestellingProducts_ProductId",
                table: "BestellingProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellings_UserId",
                table: "Bestellings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestellingProducts");

            migrationBuilder.DropTable(
                name: "Bestellings");
        }
    }
}
