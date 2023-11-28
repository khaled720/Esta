using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class MempershipExpiryMonth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        

            migrationBuilder.AddColumn<int>(
                name: "MempershipExpiryMonth",
                table: "Constants",
                type: "int",
                nullable: false,
                defaultValue: 6);




                 }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
      

            migrationBuilder.DropColumn(
                name: "MempershipExpiryMonth",
                table: "Constants");

               }
    }
}
