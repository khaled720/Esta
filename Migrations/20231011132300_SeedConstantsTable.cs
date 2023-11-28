using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class SeedConstantsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             
            //

            migrationBuilder.InsertData(
                table: "Constants",
                columns: new[] { "Id", "MempershipFee" },
                values: new object[] { "1",1500.0 });

         }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: "1");



         }
    }
}
