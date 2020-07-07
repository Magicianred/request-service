using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class ChangeQuestionSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 4,
                column: "AdditionalData",
                value: "[{\"Key\":\"keyworkers\",\"Value\":\"Key workers\"},{\"Key\":\"somonekeyworkers\",\"Value\":\"Someone helping key workers stay safe in their role (e.g. care home residents, visitors etc.)\"},{\"Key\":\"memberspublic\",\"Value\":\"Members of the public\"}]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 4,
                column: "AdditionalData",
                value: "[{\"Key\":\"keyworkers\",\"Value\":\"Key workers\"},{\"Key\":\"somonekeyworkers\",\"Value\":\"Someone helping key workers stay safe in their role (e.g. care home residents, visitors etc.)\"},{\"Key\":\"someone\",\"Value\":\"Someone else\"}]");
        }
    }
}
