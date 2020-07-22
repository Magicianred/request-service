using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class CorrectSpacing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 5,
                column: "Name",
                value: "Are you able to pay the cost of materials for your face covering (usually £2 - £3 each)?");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 5,
                column: "Name",
                value: "Are you able to pay the cost of materials for your face covering(usually £2 - £3 each) ?");
        }
    }
}
