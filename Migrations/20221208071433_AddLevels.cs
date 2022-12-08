using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class AddLevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "levelId",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "levelId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Course_levelId",
                table: "Course",
                column: "levelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_levelId",
                table: "AspNetUsers",
                column: "levelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Levels_levelId",
                table: "AspNetUsers",
                column: "levelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Levels_levelId",
                table: "Course",
                column: "levelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Levels_levelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Levels_levelId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_levelId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_levelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "levelId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "levelId",
                table: "AspNetUsers");
        }
    }
}
