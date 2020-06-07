using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class SetRequiredToFalseforSupportRequesting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 1,
                column: "Required",
                value: false);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 2,
                column: "Required",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 1,
                column: "Required",
                value: true);

            migrationBuilder.UpdateData(
                schema: "Request",
                table: "Question",
                keyColumn: "ID",
                keyValue: 2,
                column: "Required",
                value: true);
        }
    }
}
