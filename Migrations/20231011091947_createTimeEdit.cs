﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class createTimeEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
    

            migrationBuilder.AlterColumn<string>(
                name: "CreationTime",
                table: "MempershipOrders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "MempershipOrders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

          }
    }
}
