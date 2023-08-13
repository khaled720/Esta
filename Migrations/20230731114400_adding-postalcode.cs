using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class addingpostalcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
             name: "PostalCode",
             table: "AspNetUsers",
             type: "nvarchar(max)",
             nullable: false,
             defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AspNetUsers");

          }
    }
}
