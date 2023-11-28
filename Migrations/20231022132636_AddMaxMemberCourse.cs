using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class AddMaxMemberCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<int>(
                name: "MaxAllowedMembersCount",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 100);

      }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "MaxAllowedMembersCount",
                table: "Courses");
     }
    }
}
