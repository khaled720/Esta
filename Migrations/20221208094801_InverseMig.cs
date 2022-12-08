using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class InverseMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Levels_levelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Levels_levelId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_AspNetUsers_userId",
                table: "UserCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_Course_courseId",
                table: "UserCourses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_levelId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ForumLevelId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameColumn(
                name: "levelId",
                table: "AspNetUsers",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_levelId",
                table: "Courses",
                newName: "IX_Courses_levelId");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "UserCourses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LevelId",
                table: "AspNetUsers",
                column: "LevelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Levels_LevelId",
                table: "AspNetUsers",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Levels_levelId",
                table: "Courses",
                column: "levelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_AspNetUsers_userId",
                table: "UserCourses",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_Courses_courseId",
                table: "UserCourses",
                column: "courseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Levels_LevelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Levels_levelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_AspNetUsers_userId",
                table: "UserCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_Courses_courseId",
                table: "UserCourses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LevelId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "AspNetUsers",
                newName: "levelId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_levelId",
                table: "Course",
                newName: "IX_Course_levelId");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "UserCourses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ForumLevelId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");

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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_AspNetUsers_userId",
                table: "UserCourses",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_Course_courseId",
                table: "UserCourses",
                column: "courseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
