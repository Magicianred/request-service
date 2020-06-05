using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddQuestionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                schema: "Request",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    QuestionType = table.Column<byte>(nullable: false),
                    Required = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ActivityQuestions",
                schema: "Request",
                columns: table => new
                {
                    ActivityID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityQuestions", x => new { x.ActivityID, x.QuestionID });
                    table.ForeignKey(
                        name: "FK_ActivityQuestions_Question_QuestionID",
                        column: x => x.QuestionID,
                        principalSchema: "Request",
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                        principalSchema: "Request",
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
                name: "IX_ActivityQuestions_QuestionID",
                schema: "Request",
                table: "ActivityQuestions",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestQuestions_QuestionID",
                schema: "Request",
                table: "RequestQuestions",
                column: "QuestionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityQuestions",
                schema: "Request");

            migrationBuilder.DropTable(
                name: "RequestQuestions",
                schema: "Request");

            migrationBuilder.DropTable(
                name: "Question",
                schema: "Request");
        }
    }
}
