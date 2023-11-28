using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class AddNewState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "StateName" },
                values: new object[] { 4, "Refunded" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 4);

       }
    }
}
