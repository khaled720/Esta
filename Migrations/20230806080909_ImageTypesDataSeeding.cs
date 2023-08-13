using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class ImageTypesDataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.InsertData(
                table: "ImageTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "NationalId" },
                    { 2, "Passport" },
                    { 3, "Gradution" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DeleteData(
                table: "ImageTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ImageTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ImageTypes",
                keyColumn: "Id",
                keyValue: 3);

          }
    }
}
