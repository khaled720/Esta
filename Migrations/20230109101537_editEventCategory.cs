using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class editEventCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "096edd9c-5f2d-409c-b231-7e95ee741064");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9bccd5d-84c5-4e9d-91af-bd614079e4ea");

            migrationBuilder.AlterColumn<int>(
                name: "EventType",
                table: "EventsNews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af2e99c7-7674-49de-8ccd-3e07353f6e6d", "c1634d2f-d668-47ef-89be-495d687bd337", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d9971ff2-577e-4fee-a728-8e6b6f32fadc", "b60f60dc-597f-4ef1-b810-2943d65bdedc", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af2e99c7-7674-49de-8ccd-3e07353f6e6d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9971ff2-577e-4fee-a728-8e6b6f32fadc");

            migrationBuilder.AlterColumn<int>(
                name: "EventType",
                table: "EventsNews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "096edd9c-5f2d-409c-b231-7e95ee741064", "bf94b753-142d-47f2-9ff4-8d7ca3f3fa14", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9bccd5d-84c5-4e9d-91af-bd614079e4ea", "b49e9f81-ddbf-427d-8aa3-bc78676d1d99", "User", "USER" });
        }
    }
}
