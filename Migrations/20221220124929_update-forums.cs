using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class updateforums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formus_Levels_levelId",
                table: "Formus");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersForums_AspNetUsers_userId",
                table: "UsersForums");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersForums_Formus_forumId",
                table: "UsersForums");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LevelId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Formus",
                table: "Formus");

            migrationBuilder.RenameTable(
                name: "Formus",
                newName: "Forums");

            migrationBuilder.RenameColumn(
                name: "levelId",
                table: "Forums",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Formus_levelId",
                table: "Forums",
                newName: "IX_Forums_LevelId");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "UsersForums",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "UsersForums",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Forums",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Forums",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Forums",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forums",
                table: "Forums",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersForums_ParentId",
                table: "UsersForums",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LevelId",
                table: "AspNetUsers",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forums_Levels_LevelId",
                table: "Forums",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersForums_AspNetUsers_userId",
                table: "UsersForums",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersForums_Forums_forumId",
                table: "UsersForums",
                column: "forumId",
                principalTable: "Forums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersForums_UsersForums_ParentId",
                table: "UsersForums",
                column: "ParentId",
                principalTable: "UsersForums",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forums_Levels_LevelId",
                table: "Forums");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersForums_AspNetUsers_userId",
                table: "UsersForums");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersForums_Forums_forumId",
                table: "UsersForums");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersForums_UsersForums_ParentId",
                table: "UsersForums");

            migrationBuilder.DropIndex(
                name: "IX_UsersForums_ParentId",
                table: "UsersForums");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LevelId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forums",
                table: "Forums");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "UsersForums");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Forums");

            migrationBuilder.RenameTable(
                name: "Forums",
                newName: "Formus");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Formus",
                newName: "levelId");

            migrationBuilder.RenameIndex(
                name: "IX_Forums_LevelId",
                table: "Formus",
                newName: "IX_Formus_levelId");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "UsersForums",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Formus",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Formus",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Formus",
                table: "Formus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LevelId",
                table: "AspNetUsers",
                column: "LevelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Formus_Levels_levelId",
                table: "Formus",
                column: "levelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersForums_AspNetUsers_userId",
                table: "UsersForums",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersForums_Formus_forumId",
                table: "UsersForums",
                column: "forumId",
                principalTable: "Formus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
