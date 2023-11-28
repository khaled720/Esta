using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class AddingCreateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.AddColumn<string>(
                name: "CreateDate",
                table: "RefundRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

         }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "RefundRequests");
   }
    }
}
