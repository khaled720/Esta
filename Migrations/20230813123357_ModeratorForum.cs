using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class ModeratorForum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "ModeratorForums",
                columns: table => new
                {
                    ForumId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeratorForums", x => new { x.ForumId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ModeratorForums_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModeratorForums_Forums_ForumId",
                        column: x => x.ForumId,
                        principalTable: "Forums",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bfe3a8d2-7b18-4d35-91e4-0bb20ba746be", "25bb443c-6859-4061-80d2-c759e6fd3d90", "Moderator", "MODERATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_ModeratorForums_UserId",
                table: "ModeratorForums",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModeratorForums");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfe3a8d2-7b18-4d35-91e4-0bb20ba746be");
        }
    }
}
