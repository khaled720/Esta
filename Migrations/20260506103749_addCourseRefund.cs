using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class addCourseRefund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "RefundRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "RefundRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RefundRequests_CourseId",
                table: "RefundRequests",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefundRequests_Courses_CourseId",
                table: "RefundRequests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefundRequests_Courses_CourseId",
                table: "RefundRequests");

            migrationBuilder.DropIndex(
                name: "IX_RefundRequests_CourseId",
                table: "RefundRequests");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "RefundRequests");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "RefundRequests");
        }
    }
}
