using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class removingworkingLeavingDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "WorkLeavingDate",
                table: "AspNetUsers");

       }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
        name: "WorkLeavingDate",
        table: "AspNetUsers",
        type: "datetime2",
        nullable: true);
        }
    }
}
