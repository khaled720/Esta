using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class REMOVEORDERPROPS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "MempershipPayments");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "MempershipPayments");

            migrationBuilder.DropColumn(
                name: "OrderDescription",
                table: "MempershipPayments");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "MempershipPayments");

            migrationBuilder.DropColumn(
                name: "OrderReference",
                table: "MempershipPayments");

            migrationBuilder.DropColumn(
                name: "OrderResult",
                table: "MempershipPayments");

            migrationBuilder.DropColumn(
                name: "PostOrderJsonResponse",
                table: "MempershipPayments");

            migrationBuilder.DropColumn(
                name: "PrepareJsonResponse",
                table: "MempershipPayments");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "MempershipPayments");

            migrationBuilder.DropColumn(
                name: "SuccessIndicator",
                table: "MempershipPayments");

            migrationBuilder.DropColumn(
                name: "TransactionReference",
                table: "MempershipPayments");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "CoursesPayments");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "CoursesPayments");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "MempershipPayments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "MempershipPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderDescription",
                table: "MempershipPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "MempershipPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderReference",
                table: "MempershipPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderResult",
                table: "MempershipPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostOrderJsonResponse",
                table: "MempershipPayments",
                type: "ntext",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrepareJsonResponse",
                table: "MempershipPayments",
                type: "ntext",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "MempershipPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SuccessIndicator",
                table: "MempershipPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionReference",
                table: "MempershipPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "CoursesPayments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "CoursesPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

          }
    }
}
