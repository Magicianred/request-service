using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class NewQuestionSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "QuestionSet");

            migrationBuilder.RenameTable(
                name: "Question",
                schema: "Request",
                newName: "Question",
                newSchema: "QuestionSet");

            migrationBuilder.RenameTable(
                name: "ActivityQuestions",
                schema: "Request",
                newName: "ActivityQuestions",
                newSchema: "QuestionSet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Question",
                schema: "QuestionSet",
                newName: "Question",
                newSchema: "Request");

            migrationBuilder.RenameTable(
                name: "ActivityQuestions",
                schema: "QuestionSet",
                newName: "ActivityQuestions",
                newSchema: "Request");
        }
    }
}
