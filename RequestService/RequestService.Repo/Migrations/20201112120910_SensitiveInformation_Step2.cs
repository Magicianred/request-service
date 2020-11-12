using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class SensitiveInformation_Step2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                columns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID", "Location", "Order", "PlaceholderText", "RequestFormStageID", "Required", "Subtext" },
                values: new object[,]
                {
                    { 11, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 10, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 9, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 8, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 5, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 13, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 1, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 2, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 3, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 4, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 7, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 6, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 12, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 4, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 2, 14, 9, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 2, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 1, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 12, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 5, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 11, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 10, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 9, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 8, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 7, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 6, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 5, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 4, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 3, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 2, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 3, 14, 2, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 6, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 8, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 11, 14, 9, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 3, 14, 7, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 4, 14, 7, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 5, 14, 7, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 6, 14, 7, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 7, 14, 7, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 8, 14, 7, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 10, 14, 7, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 11, 14, 7, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 12, 14, 7, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 1, 14, 8, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 2, 14, 8, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 11, 14, 8, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 16, 14, 8, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 15, 14, 8, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 1, 14, 9, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 2, 14, 7, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 1, 14, 7, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 14, 14, 6, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 12, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 9, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 10, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 11, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 12, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 12, 14, 4, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 1, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 2, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 7, 14, 3, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 3, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 5, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 6, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 7, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 8, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 9, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 10, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 11, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 4, 14, 5, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null },
                    { 1, 14, 1, "details2", 3, "For example, phone John on XXXXX when you’re ten minutes away. Or use code 1234 to access the main gate etc.", 2, false, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 14, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 14, 8 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 1, 14, 9 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 14, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 14, 8 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 2, 14, 9 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 3, 14, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 4, 14, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 5, 14, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 6, 14, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 7, 14, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 8, 14, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 9, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 10, 14, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 14, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 14, 8 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 11, 14, 9 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 14, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 14, 4 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 14, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 12, 14, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 13, 14, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 14, 14, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 15, 14, 8 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" },
                keyValues: new object[] { 16, 14, 8 });

        }
    }
}
