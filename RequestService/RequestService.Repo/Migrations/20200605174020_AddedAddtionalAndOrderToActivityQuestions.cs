using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddedAddtionalAndOrderToActivityQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalData",
                schema: "Request",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalData",
                schema: "Request",
                table: "ActivityQuestions",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "Request",
                table: "ActivityQuestions",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 1,
                column: "QuestionType",
                value: (byte)3);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 2,
                column: "QuestionType",
                value: (byte)3);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 4,
                column: "QuestionType",
                value: (byte)4);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "Name", "QuestionType" },
                values: new object[] { "Are you able to pay the cost of materials for your face covering (usually £2 - £3 each)?", (byte)4 });

            migrationBuilder.InsertData(
                schema: "Request",
                table: "Question",
                columns: new[] { "ID", "AdditionalData", "Name", "QuestionType", "Required" },
                values: new object[] { 6, null, "Is this request critical to someone’s health or wellbeing?", (byte)4, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "AdditionalData",
                schema: "Request",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "AdditionalData",
                schema: "Request",
                table: "ActivityQuestions");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "Request",
                table: "ActivityQuestions");

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 1,
                column: "QuestionType",
                value: (byte)2);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 2,
                column: "QuestionType",
                value: (byte)2);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 4,
                column: "QuestionType",
                value: (byte)2);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "Name", "QuestionType" },
                values: new object[] { "Are you able to pay the cost of materials for your face covering (usually £2 - £3 each)? Volunteers are providing their time and skills free of charge", (byte)2 });
        }
    }
}
