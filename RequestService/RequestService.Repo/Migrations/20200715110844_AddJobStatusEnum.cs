using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddJobStatusEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobStatus",
                schema: "Lookup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "JobStatus",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Open" });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "JobStatus",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "InProgress" });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "JobStatus",
                columns: new[] { "ID", "Name" },
                values: new object[] { 3, "Done" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobStatus",
                schema: "Lookup");
        }
    }
}
