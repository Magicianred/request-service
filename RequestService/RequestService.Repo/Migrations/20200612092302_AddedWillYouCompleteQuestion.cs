using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddedWillYouCompleteQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "Question",
                columns: new[] { "ID", "AdditionalData", "Name", "QuestionType", "Required" },
                values: new object[] { 7, "[{\"Key\":\"true\",\"Value\":\"Yes\"},{\"Key\":\"false\",\"Value\":\"No, please make it visible to other volunteers\"}]", "Will you complete this request yourself?", (byte)4, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 7);
        }
    }
}
