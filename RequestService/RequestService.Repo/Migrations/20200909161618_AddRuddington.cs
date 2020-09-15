using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddRuddington : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "RequestFormVariant",
                columns: new[] { "ID", "Name" },
                values: new object[] { 7, "Ruddington" });

            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                columns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID", "Location", "Order", "PlaceholderText", "RequestFormStageID", "Required", "Subtext" },
                values: new object[,]
                {
                    { 7, 10, 7, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", 2, false, null },
                    { 7, 6, 7, "pos3", 2, null, 1, true, null },
                    { 7, 9, 7, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", 2, false, null },
                    { 8, 1, 7, "pos1", 1, "Please don’t include any sensitive details that aren’t needed in order for us to help you", 1, false, null },
                    { 8, 10, 7, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", 2, false, null },
                    { 8, 6, 7, "pos3", 2, null, 1, true, null },
                    { 8, 9, 7, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", 2, false, null },
                    { 10, 1, 7, "pos1", 1, "Please don’t include any sensitive details that aren’t needed in order for us to help you", 1, false, null },
                    { 10, 10, 7, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", 2, false, null },
                    { 10, 6, 7, "pos3", 2, null, 1, true, null },
                    { 10, 9, 7, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", 2, false, null },
                    { 11, 1, 7, "pos1", 1, "Please don’t include any sensitive details that aren’t needed in order for us to help you", 1, false, null },
                    { 11, 10, 7, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", 2, false, null },
                    { 11, 6, 7, "pos3", 2, null, 1, true, null },
                    { 11, 9, 7, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", 2, false, null },
                    { 12, 2, 7, "pos2", 2, "Don’t forget to tell us how many of each size you need. If you have very specific style requirements it may take longer to find a volunteer to help with your request. Please don’t include personal information such as name or address in this box, we’ll ask for that later.", 1, false, "Size guide:<br />&nbsp;- Men’s (Small / Medium / Large)<br />&nbsp;- Ladies’ (Small / Medium / Large)<br />&nbsp;- Children’s (One Size - under 12)" },
                    { 12, 3, 7, "pos3", 1, null, 1, true, "Remember they’re washable and reusable, so only request what you need between washes." },
                    { 12, 4, 7, "pos3", 3, null, 1, false, null },
                    { 12, 5, 7, "pos3", 4, null, 1, false, "Volunteers are providing their time and skills free of charge." },
                    { 7, 1, 7, "pos1", 1, "Please don’t include any sensitive details that aren’t needed in order for us to help you", 1, false, null },
                    { 6, 9, 7, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", 2, false, null },
                    { 6, 6, 7, "pos3", 2, null, 1, true, null },
                    { 6, 10, 7, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", 2, false, null },
                    { 1, 10, 7, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", 2, false, null },
                    { 1, 6, 7, "pos3", 2, null, 1, true, null },
                    { 1, 9, 7, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", 2, false, null },
                    { 2, 1, 7, "pos1", 1, "Please don’t include any sensitive details that aren’t needed in order for us to help you", 1, false, null },
                    { 2, 10, 7, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", 2, false, null },
                    { 2, 6, 7, "pos3", 2, null, 1, true, null },
                    { 2, 9, 7, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", 2, false, null },
                    { 3, 1, 7, "pos1", 1, "Please don’t include any sensitive details that aren’t needed in order for us to help you", 1, false, null },
                    { 3, 10, 7, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", 2, false, null },
                    { 12, 9, 7, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", 2, false, null },
                    { 3, 6, 7, "pos3", 2, null, 1, true, null },
                    { 4, 1, 7, "pos1", 1, "Please don’t include any sensitive details that aren’t needed in order for us to help you", 1, false, null },
                    { 4, 10, 7, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", 2, false, null },
                    { 4, 6, 7, "pos3", 2, null, 1, true, null },
                    { 4, 9, 7, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", 2, false, null },
                    { 5, 1, 7, "pos1", 1, "Please don’t include any sensitive details that aren’t needed in order for us to help you", 1, false, null },
                    { 5, 10, 7, "details2", 2, "For example, if it’s a request for some shopping and you know what you want, you could give us the list.", 2, false, null },
                    { 5, 6, 7, "pos3", 2, null, 1, true, null },
                    { 5, 9, 7, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", 2, false, null },
                    { 6, 1, 7, "pos1", 1, "Please don’t include any sensitive details that aren’t needed in order for us to help you", 1, false, null },
                    { 3, 9, 7, "details2", 1, "For example, do you have any specific language requirement or hearing issues that we should know about?", 2, false, null },
                    { 1, 1, 7, "pos1", 1, "Please don’t include any sensitive details that aren’t needed in order for us to help you", 1, false, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Lookup",
                table: "RequestFormVariant",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 1, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 6, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 9, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 10, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 1, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 6, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 9, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 10, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 1, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 6, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 9, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 10, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 1, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 6, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 9, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 10, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 1, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 6, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 9, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 10, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 1, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 6, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 9, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 10, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 1, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 6, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 9, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 10, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 1, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 6, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 9, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 10, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 1, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 6, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 9, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 10, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 1, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 6, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 9, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 10, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 2, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 3, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 4, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 5, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 9, 7 });
        }
    }
}
