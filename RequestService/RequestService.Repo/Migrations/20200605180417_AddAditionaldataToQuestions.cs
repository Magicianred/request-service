using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddAditionaldataToQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalData",
                schema: "Request",
                table: "ActivityQuestions");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalData",
                schema: "Request",
                table: "Question",
                unicode: false,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalData",
                schema: "Request",
                table: "Question");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalData",
                schema: "Request",
                table: "ActivityQuestions",
                unicode: false,
                nullable: true);
        }
    }
}
