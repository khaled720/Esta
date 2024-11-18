using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class addDetailsToBanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DetailsAr",
                table: "HomeBanners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DetailsEn",
                table: "HomeBanners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
            }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "DetailsAr",
                table: "HomeBanners");

            migrationBuilder.DropColumn(
                name: "DetailsEn",
                table: "HomeBanners");
        }
    }
}
