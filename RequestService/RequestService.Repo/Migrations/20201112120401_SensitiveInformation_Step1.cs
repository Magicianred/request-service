using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class SensitiveInformation_Step1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AnswerContainsSensitiveData",
                schema: "QuestionSet",
                table: "Question",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "Question",
                columns: new[] { "ID", "Name" },
                values: new object[] { 14, "SensitiveInformation" });

            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "Question",
                columns: new[] { "ID", "AdditionalData", "AnswerContainsSensitiveData", "Name", "QuestionType" },
                values: new object[] { 14, "", true, "Any sensitve information to share?", (byte)3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Lookup",
                table: "Question",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DropColumn(
                name: "AnswerContainsSensitiveData",
                schema: "QuestionSet",
                table: "Question");            
        }
    }
}
