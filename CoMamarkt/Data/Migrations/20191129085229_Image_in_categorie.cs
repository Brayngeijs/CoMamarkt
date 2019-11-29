using Microsoft.EntityFrameworkCore.Migrations;

namespace CoMamarkt.Data.Migrations
{
    public partial class Image_in_categorie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Categorie",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Categorie");
        }
    }
}
