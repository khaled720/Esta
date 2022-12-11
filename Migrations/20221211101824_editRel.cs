using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class editRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Levels_levelId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "levelId",
                table: "Courses",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_levelId",
                table: "Courses",
                newName: "IX_Courses_LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Levels_LevelId",
                table: "Courses",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Levels_LevelId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Courses",
                newName: "levelId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_LevelId",
                table: "Courses",
                newName: "IX_Courses_levelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Levels_levelId",
                table: "Courses",
                column: "levelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
