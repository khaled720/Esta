using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class addingUserImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.AddColumn<string>(
                name: "GradutionImagePath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MembershipNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalIDImagePath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassportImagePath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
     

            migrationBuilder.DropColumn(
                name: "GradutionImagePath",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MembershipNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NationalIDImagePath",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PassportImagePath",
                table: "AspNetUsers");

    
        }
    }
}
