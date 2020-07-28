using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddCancelledJobStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "JobStatus",
                columns: new[] { "ID", "Name" },
                values: new object[] { 4, "Cancelled" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Lookup",
                table: "JobStatus",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
