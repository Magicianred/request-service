using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class ChangeQuestionToJobLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestQuestions",
                schema: "Request");

            migrationBuilder.CreateTable(
                name: "JobQuestions",
                schema: "Request",
                columns: table => new
                {
                    JobID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobQuestions", x => new { x.JobID, x.QuestionID });
                    table.ForeignKey(
                        name: "FK_JobQuestions_Job_JobID",
                        column: x => x.JobID,
                        principalSchema: "Request",
                        principalTable: "Job",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobQuestions_Question_QuestionID",
                        column: x => x.QuestionID,
                        principalSchema: "QuestionSet",
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobQuestions_QuestionID",
                schema: "Request",
                table: "JobQuestions",
                column: "QuestionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobQuestions",
                schema: "Request");

            migrationBuilder.CreateTable(
                name: "RequestQuestions",
                schema: "Request",
                columns: table => new
                {
                    RequestID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestQuestions", x => new { x.RequestID, x.QuestionID });
                    table.ForeignKey(
                        name: "FK_RequestQuestions_Question_QuestionID",
                        column: x => x.QuestionID,
                        principalSchema: "QuestionSet",
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestQuestions_Request_RequestID",
                        column: x => x.RequestID,
                        principalSchema: "Request",
                        principalTable: "Request",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestQuestions_QuestionID",
                schema: "Request",
                table: "RequestQuestions",
                column: "QuestionID");
        }
    }
}
