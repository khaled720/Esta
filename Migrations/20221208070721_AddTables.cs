using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForumLevelId",
                table: "Formus");

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalGrade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    courseId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isPaid = table.Column<bool>(type: "bit", nullable: false),
                    stateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCourses_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_Course_courseId",
                        column: x => x.courseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_States_stateId",
                        column: x => x.stateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_courseId",
                table: "UserCourses",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_stateId",
                table: "UserCourses",
                column: "stateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_userId",
                table: "UserCourses",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.AddColumn<int>(
                name: "ForumLevelId",
                table: "Formus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
