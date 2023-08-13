using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class removingworkingLeavingReasons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
   
            migrationBuilder.DropColumn(
                name: "WorkLeavingReasons",
                table: "AspNetUsers");

            }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
      

            migrationBuilder.AddColumn<string>(
                name: "WorkLeavingReasons",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

           }
    }
}
