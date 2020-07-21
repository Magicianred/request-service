using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddRequestFormSpecificQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityQuestions",
                schema: "QuestionSet",
                table: "ActivityQuestions");

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 8, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 9, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 9, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 10, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 10, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 11, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 11, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 2 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 5 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 13, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 13, 7 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 14, 6 });

            migrationBuilder.DeleteData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 14, 7 });

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "QuestionSet",
                table: "Question");

            migrationBuilder.AddColumn<int>(
                name: "RequestFormVariantID",
                schema: "QuestionSet",
                table: "ActivityQuestions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "QuestionSet",
                table: "ActivityQuestions",
                unicode: false,
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityQuestions",
                schema: "QuestionSet",
                table: "ActivityQuestions",
                columns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID" });

            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                columns: new[] { "ActivityID", "QuestionID", "RequestFormVariantID", "Name", "Order" },
                values: new object[,]
                {
                    { 1, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 10, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 10, 7, 2, "Will you complete this request yourself?", 3 },
                    { 10, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 10, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 10, 7, 3, "Will you complete this request yourself?", 3 },
                    { 10, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 10, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 10, 7, 4, "Will you complete this request yourself?", 3 },
                    { 10, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 11, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 11, 7, 1, "Will you complete this request yourself?", 3 },
                    { 11, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 11, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 11, 7, 2, "Will you complete this request yourself?", 3 },
                    { 11, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 11, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 11, 7, 3, "Will you complete this request yourself?", 3 },
                    { 11, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 11, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 11, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 10, 7, 1, "Will you complete this request yourself?", 3 },
                    { 10, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 8, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 8, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 8, 7, 3, "Will you complete this request yourself?", 3 },
                    { 8, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 8, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 8, 7, 4, "Will you complete this request yourself?", 3 },
                    { 9, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 9, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 10, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 9, 7, 1, "Will you complete this request yourself?", 3 },
                    { 9, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 9, 7, 2, "Will you complete this request yourself?", 3 },
                    { 9, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 9, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 9, 7, 3, "Will you complete this request yourself?", 3 },
                    { 9, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 9, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 9, 7, 4, "Will you complete this request yourself?", 3 },
                    { 9, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 8, 7, 2, "Will you complete this request yourself?", 3 },
                    { 11, 7, 4, "Will you complete this request yourself?", 3 },
                    { 12, 3, 1, "How many face coverings do you need?", 1 },
                    { 13, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 13, 7, 2, "Will you complete this request yourself?", 3 },
                    { 13, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 13, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 13, 7, 3, "Will you complete this request yourself?", 3 },
                    { 13, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 13, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 13, 7, 4, "Will you complete this request yourself?", 3 },
                    { 13, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 14, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 14, 7, 1, "Will you complete this request yourself?", 3 },
                    { 14, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 14, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 14, 7, 2, "Will you complete this request yourself?", 3 },
                    { 14, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 14, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 14, 7, 3, "Will you complete this request yourself?", 3 },
                    { 14, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 14, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 12, 2, 1, "Please tell us about any specific requirements (e.g. size, colour, style etc.)", 2 },
                    { 13, 7, 1, "Will you complete this request yourself?", 3 },
                    { 13, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 12, 4, 1, "Who will be using the face coverings?", 3 },
                    { 12, 5, 1, "Are you able to pay the cost of materials for your face covering (usually £2 - £3 each)?", 4 },
                    { 12, 7, 1, "Will you complete this request yourself?", 5 },
                    { 12, 2, 2, "Please tell us about any specific requirements (e.g. size, colour, style etc.)", 2 },
                    { 12, 3, 2, "How many face coverings do you need?", 1 },
                    { 12, 4, 2, "Who will be using the face coverings?", 3 },
                    { 12, 5, 2, "Are you able to pay the cost of materials for your face covering (usually £2 - £3 each)?", 4 },
                    { 12, 7, 2, "Will you complete this request yourself?", 5 },
                    { 13, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 12, 2, 3, "Please tell us about any specific requirements (e.g. size, colour, style etc.)", 2 },
                    { 12, 4, 3, "Who will be using the face coverings?", 3 },
                    { 12, 5, 3, "Are you able to pay the cost of materials for your face covering (usually £2 - £3 each)?", 4 },
                    { 12, 7, 3, "Will you complete this request yourself?", 5 },
                    { 12, 2, 4, "Please tell us about any specific requirements (e.g. size, colour, style etc.)", 2 },
                    { 12, 3, 4, "How many face coverings do you need?", 1 },
                    { 12, 4, 4, "Who will be using the face coverings?", 3 },
                    { 12, 5, 4, "Please donate to the For the Love of Scrubs GoFundMe <a href=\"https://www.gofundme.com/f/for-the-love-of-scrubs-face-coverings\" target=\"_blank\">here</a> to help pay for materials and to help us continue our good work. Recommended donation £3 - £4 per face covering.", 4 },
                    { 12, 7, 4, "Will you complete this request yourself?", 5 },
                    { 12, 3, 3, "How many face coverings do you need?", 1 },
                    { 8, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 8, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 8, 7, 1, "Will you complete this request yourself?", 3 },
                    { 2, 7, 4, "Will you complete this request yourself?", 3 },
                    { 3, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 3, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 3, 7, 1, "Will you complete this request yourself?", 3 },
                    { 3, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 3, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 3, 7, 2, "Will you complete this request yourself?", 3 },
                    { 3, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 2, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 3, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 3, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 3, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 3, 7, 4, "Will you complete this request yourself?", 3 },
                    { 4, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 4, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 4, 7, 1, "Will you complete this request yourself?", 3 },
                    { 4, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 4, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 3, 7, 3, "Will you complete this request yourself?", 3 },
                    { 4, 7, 2, "Will you complete this request yourself?", 3 },
                    { 2, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 2, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 1, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 1, 7, 1, "Will you complete this request yourself?", 3 },
                    { 1, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 1, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 1, 7, 2, "Will you complete this request yourself?", 3 },
                    { 1, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 1, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 1, 7, 3, "Will you complete this request yourself?", 3 },
                    { 2, 7, 3, "Will you complete this request yourself?", 3 },
                    { 1, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 1, 7, 4, "Will you complete this request yourself?", 3 },
                    { 2, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 2, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 2, 7, 1, "Will you complete this request yourself?", 3 },
                    { 2, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 2, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 2, 7, 2, "Will you complete this request yourself?", 3 },
                    { 2, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 1, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 4, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 4, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 4, 7, 3, "Will you complete this request yourself?", 3 },
                    { 6, 7, 3, "Will you complete this request yourself?", 3 },
                    { 6, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 6, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 6, 7, 4, "Will you complete this request yourself?", 3 },
                    { 7, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 7, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 7, 7, 1, "Will you complete this request yourself?", 3 },
                    { 7, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 6, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 7, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 7, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 7, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 7, 7, 3, "Will you complete this request yourself?", 3 },
                    { 7, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 7, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 7, 7, 4, "Will you complete this request yourself?", 3 },
                    { 8, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 8, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 7, 7, 2, "Will you complete this request yourself?", 3 },
                    { 6, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 6, 7, 2, "Will you complete this request yourself?", 3 },
                    { 6, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 4, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 4, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 4, 7, 4, "Will you complete this request yourself?", 3 },
                    { 5, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 5, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 5, 7, 1, "Will you complete this request yourself?", 3 },
                    { 5, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 5, 6, 2, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 5, 7, 2, "Will you complete this request yourself?", 3 },
                    { 5, 1, 3, "Please tell us more about the help or support you're requesting", 1 },
                    { 5, 6, 3, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 5, 7, 3, "Will you complete this request yourself?", 3 },
                    { 5, 1, 4, "Please tell us more about the help or support you're requesting", 1 },
                    { 5, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 5, 7, 4, "Will you complete this request yourself?", 3 },
                    { 6, 1, 1, "Please tell us more about the help or support you're requesting", 1 },
                    { 6, 6, 1, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 6, 7, 1, "Will you complete this request yourself?", 3 },
                    { 6, 1, 2, "Please tell us more about the help or support you're requesting", 1 },
                    { 14, 6, 4, "Is this request critical to someone's health or wellbeing?", 2 },
                    { 14, 7, 4, "Will you complete this request yourself?", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityQuestions",
                schema: "QuestionSet",
                table: "ActivityQuestions");

            migrationBuilder.DropColumn(
                name: "RequestFormVariantID",
                schema: "QuestionSet",
                table: "ActivityQuestions");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "QuestionSet",
                table: "ActivityQuestions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "QuestionSet",
                table: "Question",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityQuestions",
                schema: "QuestionSet",
                table: "ActivityQuestions",
                columns: new[] { "ActivityID", "QuestionID" });

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 1, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 1, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 1, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 2, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 2, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 2, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 3, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 3, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 3, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 4, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 4, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 4, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 5, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 5, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 5, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 6, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 6, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 6, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 7, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 7, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 7, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 8, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 8, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 8, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 9, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 9, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 9, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 10, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 10, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 10, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 11, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 11, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 11, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 2 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 3 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 4 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 5 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 12, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 13, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 13, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 13, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 14, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 14, 6 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                keyColumns: new[] { "ActivityID", "QuestionID" },
                keyValues: new object[] { 14, 7 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Please tell us more about the help or support you're requesting");

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 2,
                column: "Name",
                value: "Please tell us about any specific requirements (e.g. size, colour, style etc.)");

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 3,
                column: "Name",
                value: "How many face coverings do you need?");

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 4,
                column: "Name",
                value: "Who will be using the face coverings?");

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 5,
                column: "Name",
                value: "Please donate to the For the Love of Scrubs GoFundMe <a href=\"https://www.gofundme.com/f/for-the-love-of-scrubs-face-coverings\">here</a> to help pay for materials and to help us continue our good work. Recommended donation £3 - £4 per face covering.");

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 6,
                column: "Name",
                value: "Is this request critical to someone's health or wellbeing?");

            migrationBuilder.UpdateData(
                schema: "QuestionSet",
                table: "Question",
                keyColumn: "ID",
                keyValue: 7,
                column: "Name",
                value: "Will you complete this request yourself?");
        }
    }
}
