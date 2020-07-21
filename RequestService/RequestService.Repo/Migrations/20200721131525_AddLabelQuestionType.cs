using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddLabelQuestionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "Question",
                columns: new[] { "ID", "AdditionalData", "Name", "QuestionType" },
                values: new object[] { 8, "", "Please donate to the For the Love of Scrubs GoFundMe <a href=\"https://www.gofundme.com/f/for-the-love-of-scrubs-face-coverings\" target=\"_blank\">here</a> to help pay for materials and to help us continue our good work. Recommended donation £3 - £4 per face covering.", (byte)5 });

            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                columns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID", "Order", "Required" },
                values: new object[] { 12, 8, 4, 4, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 8, 4 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 8);
        }
    }
}
