using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class CreateLookupQuestionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionType",
                schema: "Lookup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionType", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "QuestionType",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Number" },
                    { 2, "Text" },
                    { 3, "MultiLineText" },
                    { 4, "Radio" },
                    { 5, "LabelOnly" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionType",
                schema: "Lookup");
        }
    }
}
