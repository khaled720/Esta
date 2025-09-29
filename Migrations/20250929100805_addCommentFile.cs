using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class addCommentFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "UsersForums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "UsersForums");
        }
    }
}
