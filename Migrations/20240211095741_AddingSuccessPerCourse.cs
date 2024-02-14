using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class AddingSuccessPerCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<int>(
                name: "SuccessPersentage",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 50);

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropColumn(
                name: "SuccessPersentage",
                table: "Courses");


        }
    }
}
