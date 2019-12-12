using Microsoft.EntityFrameworkCore.Migrations;

namespace CoMamarkt.Data.Migrations
{
    public partial class idk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BannerURL",
                table: "Categorie",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerURL",
                table: "Categorie");
        }
    }
}
