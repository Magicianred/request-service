using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddGroupAndSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReferringGroupId",
                schema: "Request",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                schema: "Request",
                table: "Request",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobAvailableToGroup",
                schema: "Request",
                columns: table => new
                {
                    JobID = table.Column<int>(nullable: false),
                    GroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAvailableToGroup", x => new { x.JobID, x.GroupID });
                    table.ForeignKey(
                        name: "FK_JobAvailableToGroup_JobID",
                        column: x => x.JobID,
                        principalSchema: "Request",
                        principalTable: "Job",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                columns: new[] { "ActivityID", "QuestionID", "Order" },
                values: new object[] { 14, 1, 1 });

            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                columns: new[] { "ActivityID", "QuestionID", "Order" },
                values: new object[] { 14, 6, 2 });

            migrationBuilder.InsertData(
                schema: "QuestionSet",
                table: "ActivityQuestions",
                columns: new[] { "ActivityID", "QuestionID", "Order" },
                values: new object[] { 14, 7, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobAvailableToGroup",
                schema: "Request");

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
                name: "ReferringGroupId",
                schema: "Request",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Source",
                schema: "Request",
                table: "Request");
        }
    }
}
