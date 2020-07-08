using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class ChangeQuestionSetOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 2 },
                column: "Order",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 3 },
                column: "Order",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 2,
                column: "Name",
                value: "Please tell us about any specific requirements (e.g. size, colour, style etc.)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 2 },
                column: "Order",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 3 },
                column: "Order",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 2,
                column: "Name",
                value: "Please tell us about any specific requirements (e.g. colour, style etc.)");
        }
    }
}
