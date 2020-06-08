using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddQuestionData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Request",
                table: "Question",
                columns: new[] { "ID", "Name", "QuestionType", "Required" },
                values: new object[,]
                {
                    { 1, "Please tell us more about the help or support you're requesting", (byte)2, true },
                    { 2, "Please tell us about any specific requirements (e.g. colour, style etc.)", (byte)2, true },
                    { 3, "How many face coverings do you need?", (byte)1, true },
                    { 4, "Who will be using the face coverings?", (byte)2, true },
                    { 5, "Are you able to pay the cost of materials for your face covering (usually £2 - £3 each)? Volunteers are providing their time and skills free of charge", (byte)2, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 5);
        }
    }
}
