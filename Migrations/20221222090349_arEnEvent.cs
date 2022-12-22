using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class arEnEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "EventsNews",
                newName: "TitleEn");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "EventsNews",
                newName: "DetailsEn");

            migrationBuilder.AddColumn<string>(
                name: "DetailsAr",
                table: "EventsNews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleAr",
                table: "EventsNews",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailsAr",
                table: "EventsNews");

            migrationBuilder.DropColumn(
                name: "TitleAr",
                table: "EventsNews");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "EventsNews",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "DetailsEn",
                table: "EventsNews",
                newName: "Details");
        }
    }
}
