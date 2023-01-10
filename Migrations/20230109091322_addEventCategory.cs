using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class addEventCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c4858b5-586d-42da-ad8c-aa25c902ceb5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a8ba475-bc2d-40ad-9a89-4444a519ed66");

            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "EventsNews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "096edd9c-5f2d-409c-b231-7e95ee741064", "bf94b753-142d-47f2-9ff4-8d7ca3f3fa14", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9bccd5d-84c5-4e9d-91af-bd614079e4ea", "b49e9f81-ddbf-427d-8aa3-bc78676d1d99", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "096edd9c-5f2d-409c-b231-7e95ee741064");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9bccd5d-84c5-4e9d-91af-bd614079e4ea");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "EventsNews");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5c4858b5-586d-42da-ad8c-aa25c902ceb5", "b97df0cd-f867-424f-bdba-c71eb1a46396", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6a8ba475-bc2d-40ad-9a89-4444a519ed66", "34acf6d7-2fe5-4f27-bf7e-8f9e6c4c4af7", "Admin", "ADMIN" });
        }
    }
}
