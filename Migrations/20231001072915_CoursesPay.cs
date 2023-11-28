using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class CoursesPay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
     
            migrationBuilder.CreateTable(
                name: "CoursesOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    OrderDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrepareJsonResponse = table.Column<string>(type: "ntext", nullable: true),
                    OrderResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuccessIndicator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostOrderJsonResponse = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursesOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CoursesOrders_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CoursesPayments",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ResultJsonResponse = table.Column<string>(type: "ntext", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOnCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAuthorizedAmount = table.Column<double>(type: "float", nullable: false),
                    TotalCapturedAmount = table.Column<double>(type: "float", nullable: false),
                    TotalRefundedAmount = table.Column<double>(type: "float", nullable: false),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesPayments", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_CoursesPayments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CoursesPayments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CoursesPayments_CoursesOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CoursesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });


            migrationBuilder.CreateIndex(
                name: "IX_CoursesOrders_CourseId",
                table: "CoursesOrders",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesOrders_UserId",
                table: "CoursesOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesPayments_CourseId",
                table: "CoursesPayments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesPayments_UserId",
                table: "CoursesPayments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesPayments");

            migrationBuilder.DropTable(
                name: "CoursesOrders");

        
        }
    }
}
