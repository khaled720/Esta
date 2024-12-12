using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class updateConstants2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LatePenalty",
                table: "Constants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NewMempershipFee",
                table: "Constants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RenewalFee",
                table: "Constants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatePenalty",
                table: "Constants");

            migrationBuilder.DropColumn(
                name: "NewMempershipFee",
                table: "Constants");

            migrationBuilder.DropColumn(
                name: "RenewalFee",
                table: "Constants");
        }
    }
}
