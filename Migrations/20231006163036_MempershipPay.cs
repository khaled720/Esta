using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class MempershipPay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "MempershipOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    OrderDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepareJsonResponse = table.Column<string>(type: "ntext", nullable: false),
                    OrderResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuccessIndicator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostOrderJsonResponse = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MempershipOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MempershipOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MempershipPayments",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    OrderDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepareJsonResponse = table.Column<string>(type: "ntext", nullable: false),
                    OrderResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuccessIndicator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostOrderJsonResponse = table.Column<string>(type: "ntext", nullable: false),
                    TotalAuthorizedAmount = table.Column<double>(type: "float", nullable: false),
                    TotalCapturedAmount = table.Column<double>(type: "float", nullable: false),
                    TotalRefundedAmount = table.Column<double>(type: "float", nullable: false),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOnCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultJsonResponse = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MempershipPayments", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_MempershipPayments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MempershipPayments_MempershipOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "MempershipOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

         
            migrationBuilder.CreateIndex(
                name: "IX_MempershipOrders_UserId",
                table: "MempershipOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MempershipPayments_UserId",
                table: "MempershipPayments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MempershipPayments");

            migrationBuilder.DropTable(
                name: "MempershipOrders");

            }
    }
}
