using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class addgraduationyear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        

            migrationBuilder.AddColumn<string>(
                name: "GradutionYear",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
     }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
   
            migrationBuilder.DropColumn(
                name: "GradutionYear",
                table: "AspNetUsers");
   }
    }
}
