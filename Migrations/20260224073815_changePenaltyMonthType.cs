using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class changePenaltyMonthType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PenaltyMonth",
                table: "Constants");

            migrationBuilder.AddColumn<int>(
                name: "PenaltyMonth",
                table: "Constants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PenaltyMonth",
                table: "Constants",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.DropColumn(
                name: "PenaltyMonth",
                table: "Constants");
        }
    }
}
