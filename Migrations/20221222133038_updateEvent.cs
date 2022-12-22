using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class updateEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "EventsNews",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "EventsNews",
                newName: "Photo");
        }
    }
}
