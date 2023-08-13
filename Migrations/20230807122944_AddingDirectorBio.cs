using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class AddingDirectorBio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<string>(
                name: "BioAr",
                table: "Directors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BioEn",
                table: "Directors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "BioAr",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "BioEn",
                table: "Directors");

                 }
    }
}
