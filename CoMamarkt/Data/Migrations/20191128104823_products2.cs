using Microsoft.EntityFrameworkCore.Migrations;

namespace CoMamarkt.Data.Migrations
{
    public partial class products2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categorie_CategorieId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Subcategorie_SubcategorieId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Subsubcategorie_SubsubcategorieId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "SubsubcategorieId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategorieId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategorieId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categorie_CategorieId",
                table: "Product",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Subcategorie_SubcategorieId",
                table: "Product",
                column: "SubcategorieId",
                principalTable: "Subcategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Subsubcategorie_SubsubcategorieId",
                table: "Product",
                column: "SubsubcategorieId",
                principalTable: "Subsubcategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categorie_CategorieId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Subcategorie_SubcategorieId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Subsubcategorie_SubsubcategorieId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "SubsubcategorieId",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubcategorieId",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategorieId",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categorie_CategorieId",
                table: "Product",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Subcategorie_SubcategorieId",
                table: "Product",
                column: "SubcategorieId",
                principalTable: "Subcategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Subsubcategorie_SubsubcategorieId",
                table: "Product",
                column: "SubsubcategorieId",
                principalTable: "Subsubcategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
