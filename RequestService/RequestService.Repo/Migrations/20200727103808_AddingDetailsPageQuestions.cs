using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddingDetailsPageQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "Question",
                columns: new[] { "ID", "AdditionalData", "Name", "QuestionType" },
                values: new object[] { 9, "", "Are there any communication needs that volunteers need to know about before they contact you or the person who needs help?", (byte)3 });

            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "Question",
                columns: new[] { "ID", "AdditionalData", "Name", "QuestionType" },
                values: new object[] { 10, "", "Is there anything else you would like to tell us about the request?", (byte)3 });

            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                columns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID", "Location", "Order", "PlaceholderText", "Required", "Subtext" },
                values: new object[,]
                {
                    { 1, 9, 1, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 10, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 9, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 8, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 7, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 6, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 5, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 4, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 3, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 2, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 1, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 11, 10, 1, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 10, 10, 1, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 9, 10, 1, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 8, 10, 1, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 7, 10, 1, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 6, 10, 1, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 5, 10, 1, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 4, 10, 1, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 3, 10, 1, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 11, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 2, 10, 1, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 13, 10, 2, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 2, 10, 3, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 10, 10, 5, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 9, 10, 5, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 8, 10, 5, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 7, 10, 5, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 6, 10, 5, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 5, 10, 5, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 4, 10, 5, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 3, 10, 5, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 2, 10, 5, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 1, 10, 5, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 11, 10, 3, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 10, 10, 3, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 9, 10, 3, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 8, 10, 3, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 7, 10, 3, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 6, 10, 3, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 5, 10, 3, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 4, 10, 3, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 3, 10, 3, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 1, 10, 3, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 1, 10, 1, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 14, 9, 6, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 11, 9, 5, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 9, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 8, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 7, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 6, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 5, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 4, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 3, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 2, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 1, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 11, 9, 1, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 10, 9, 1, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 9, 9, 1, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 8, 9, 1, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 7, 9, 1, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 6, 9, 1, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 5, 9, 1, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 4, 9, 1, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 3, 9, 1, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 2, 9, 1, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 10, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 11, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 13, 9, 2, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 1, 9, 3, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 10, 9, 5, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 9, 9, 5, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 8, 9, 5, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 7, 9, 5, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 6, 9, 5, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 5, 9, 5, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 4, 9, 5, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 3, 9, 5, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 2, 9, 5, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 11, 10, 5, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", false, null },
                    { 1, 9, 5, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 10, 9, 3, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 9, 9, 3, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 8, 9, 3, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 7, 9, 3, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 6, 9, 3, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 5, 9, 3, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 4, 9, 3, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 3, 9, 3, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 2, 9, 3, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 11, 9, 3, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", false, null },
                    { 14, 10, 6, "details2", 2, "Is there a specific issue you would like to discuss with the Community Connector, e.g. dealing with a bereavement (please don’t include personal details here)", false, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 9, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 9, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 10, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 10, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 9, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 9, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 10, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 10, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 9, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 9, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 10, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 10, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 9, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 9, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 10, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 10, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 9, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 9, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 10, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 10, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 9, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 9, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 10, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 10, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 9, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 9, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 10, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 10, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 9, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 9, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 10, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 10, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 9, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 9, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 10, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 10, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 9, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 9, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 10, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 10, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 9, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 9, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 10, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 10, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 13, 9, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 13, 10, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 14, 9, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 14, 10, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 10);
        }
    }
}
