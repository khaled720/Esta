using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class SeedDataQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "IsYesNo", "QuestionArtxt", "QuestionEntxt" },
                values: new object[,]
                {
                    { 1, false, "كيف تعرفت على الجمعية المصرية للمحللين الفنيين؟", "How did you get to know the Egyptian Society of Technical Analysts?" },
                    { 2, false, "ماھي معلوماتك عن الجمعية المصرية للمحللين الفنيين؟", "What is your information about the Egyptian Society of Technical Analysts?" },
                    { 3, false, "لماذا ترغب في االلتحاق بالجمعية المصرية للمحللين الفنيين؟", "Why would you like to join the Egyptian Society of Technical Analysts?" },
                    { 4, false, "لو التحقت بالجمعية المصرية للمحللين الفنيين كيف يمكن أن تخدمھا؟", "If you joined the Egyptian Society of Technical Analysts, how would you serve it?" },
                    { 5, true, "ھل أنت عضو بجمعيات مماثلة سواء داخل مصر أو خارجھا؟", "Are you a member of similar Societies inside or outside Egypt?" },
                    { 6, true, "ھل لديك أي دراية بالتحليل الفني؟", "Do you have any knowledge of technical analysis?" },
                    { 7, true, "ھل لديك أي دراية بالتحليل المالي؟", "Do you have any knowledge of financial analysis?" },
                    { 8, true, "ھل أنت مستثمر بأسواق المال؟", "Are you an investor in the capital markets?" },
                    { 9, true, "ھل سبق توقيع أى عقوبات أو جزاءات عليك أو خضعت للتحقيق من ھيئة سوق المال أو\r\nالبورصة أو أى جھة رقابية أخرى داخل مصر أو خارجھا خالل الخمس سنين الماضية؟", "Have any sanctions or penalties been imposed on you or have you been investigated by the Capital Market Authority or\r\nThe stock exchange or any other regulatory body inside or outside Egypt during the past five years?" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9);


        }
    }
}
