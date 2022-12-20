using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class SeedLevelsRolesStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "65c0cea0-cea6-402f-88c8-8c1348fd6b27", "2a82b34e-a91f-42f9-9354-9fa48f0d3df0", "User", "USER" },
                    { "afdc5fd0-66a6-4fc5-b470-762885582220", "712b0e79-b4a4-4333-87c6-be58d8fc8d6b", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Ceta Level 1" },
                    { 2, "Ceta Level 2" },
                    { 3, "Ceta Level 3" },
                    { 4, "Non Ceta Level" },
                });

            migrationBuilder.InsertData(
               table: "States",
               columns: new[] { "Id", "StateName" },
               values: new object[,]
               {
                    { 1, "Enrolled" },
                    { 2, "In Progress" },
                    { 3, "Failed" },
                    { 4, "Completed" },
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65c0cea0-cea6-402f-88c8-8c1348fd6b27");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afdc5fd0-66a6-4fc5-b470-762885582220");

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 3);


            migrationBuilder.DeleteData(
               table: "States",
               keyColumn: "Id",
               keyValue: 1);
            migrationBuilder.DeleteData(
               table: "States",
               keyColumn: "Id",
               keyValue: 2);
            migrationBuilder.DeleteData(
               table: "States",
               keyColumn: "Id",
               keyValue: 3);
            migrationBuilder.DeleteData(
               table: "States",
               keyColumn: "Id",
               keyValue: 4);


        }
    }
}
