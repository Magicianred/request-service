using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddRequestorTypeToRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "RequestorType",
                schema: "Request",
                table: "Request",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "Request",
                table: "ActivityQuestions",
                columns: new[] { "ActivityID", "QuestionID", "Order" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 13, 6, 2 },
                    { 13, 1, 1 },
                    { 12, 5, 4 },
                    { 12, 4, 3 },
                    { 12, 3, 2 },
                    { 12, 2, 1 },
                    { 11, 6, 2 },
                    { 11, 1, 1 },
                    { 10, 6, 2 },
                    { 10, 1, 1 },
                    { 9, 6, 2 },
                    { 8, 6, 2 },
                    { 8, 1, 1 },
                    { 9, 1, 1 },
                    { 7, 1, 1 },
                    { 1, 6, 2 },
                    { 2, 1, 1 },
                    { 2, 6, 2 },
                    { 7, 6, 2 },
                    { 3, 6, 2 },
                    { 4, 1, 1 },
                    { 3, 1, 1 },
                    { 5, 1, 1 },
                    { 5, 6, 2 },
                    { 6, 1, 1 },
                    { 6, 6, 2 },
                    { 4, 6, 2 }
                });

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 1,
                column: "AdditionalData",
                value: "[]");

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 2,
                column: "AdditionalData",
                value: "[]");

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 3,
                column: "AdditionalData",
                value: "[]");

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "AdditionalData", "Required" },
                values: new object[] { "[{\"Key\":\"keyworkers\",\"Value\":\"Key workers\"},{\"Key\":\"somonekeyworkers\",\"Value\":\"Someone helping key workers stay safe in their role (e.g. care home residents, visitors etc.)\"},{\"Key\":\"someone\",\"Value\":\"Someone else\"}]", false });

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 5,
                column: "AdditionalData",
                value: "[{\"Key\":\"Yes\",\"Value\":\"Yes\"},{\"Key\":\"No\",\"Value\":\"No\"},{\"Key\":\"Contribution\",\"Value\":\"I can make a contribution\"}]");

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "AdditionalData", "Name", "Required" },
                values: new object[] { "[{\"Key\":\"true\",\"Value\":\"Yes\"},{\"Key\":\"false\",\"Value\":\"No\"}]", "Is this request critical to someone's health or wellbeing?", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 9, 6 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 10, 6 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 11, 6 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 2 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 5 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                schema: "Request",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 13, 6 });

            migrationBuilder.DropColumn(
                name: "RequestorType",
                schema: "Request",
                table: "Request");

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 1,
                column: "AdditionalData",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 2,
                column: "AdditionalData",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 3,
                column: "AdditionalData",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "AdditionalData", "Required" },
                values: new object[] { null, true });

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 5,
                column: "AdditionalData",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "AdditionalData", "Name", "Required" },
                values: new object[] { null, "Is this request critical to someone’s health or wellbeing?", false });
        }
    }
}
