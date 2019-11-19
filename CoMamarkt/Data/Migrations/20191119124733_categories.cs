using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoMamarkt.Data.Migrations
{
    public partial class categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bestelling",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(nullable: true),
                    Tussenvoegsel = table.Column<string>(nullable: true),
                    Achternaam = table.Column<string>(nullable: true),
                    Woonplaats = table.Column<string>(nullable: true),
                    Straat = table.Column<string>(nullable: true),
                    Huisnummer = table.Column<string>(nullable: true),
                    BestellingDatum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestelling", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcategorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true),
                    CategorieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategorie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategorie_Categorie_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subsubcategorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true),
                    SubcategorieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsubcategorie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subsubcategorie_Subcategorie_SubcategorieId",
                        column: x => x.SubcategorieId,
                        principalTable: "Subcategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategorieId = table.Column<int>(nullable: false),
                    SubcategorieId = table.Column<int>(nullable: false),
                    SubsubcategorieId = table.Column<int>(nullable: false),
                    BestellingId = table.Column<int>(nullable: false),
                    EAN = table.Column<long>(nullable: false),
                    Naam = table.Column<string>(nullable: true),
                    Merk = table.Column<string>(nullable: true),
                    KorteOmschrijving = table.Column<string>(nullable: true),
                    Omschrijving = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Gewicht = table.Column<string>(nullable: true),
                    Prijs = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Bestelling_BestellingId",
                        column: x => x.BestellingId,
                        principalTable: "Bestelling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Categorie_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Subcategorie_SubcategorieId",
                        column: x => x.SubcategorieId,
                        principalTable: "Subcategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Subsubcategorie_SubsubcategorieId",
                        column: x => x.SubsubcategorieId,
                        principalTable: "Subsubcategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_BestellingId",
                table: "Product",
                column: "BestellingId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategorieId",
                table: "Product",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SubcategorieId",
                table: "Product",
                column: "SubcategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SubsubcategorieId",
                table: "Product",
                column: "SubsubcategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategorie_CategorieId",
                table: "Subcategorie",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsubcategorie_SubcategorieId",
                table: "Subsubcategorie",
                column: "SubcategorieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Bestelling");

            migrationBuilder.DropTable(
                name: "Subsubcategorie");

            migrationBuilder.DropTable(
                name: "Subcategorie");

            migrationBuilder.DropTable(
                name: "Categorie");
        }
    }
}
