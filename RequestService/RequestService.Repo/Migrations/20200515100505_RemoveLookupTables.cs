using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class RemoveLookupTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_JobStatusID",
                schema: "Request",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportActivity_SupportActivityID",
                schema: "Request",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_JobStatus_JobStatusID",
                schema: "Request",
                table: "RequestJobStatus");

            migrationBuilder.DropTable(
                name: "JobStatus",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "SupportActivity",
                schema: "Lookup");

            migrationBuilder.DropIndex(
                name: "IX_RequestJobStatus_JobStatusID",
                schema: "Request",
                table: "RequestJobStatus");

            migrationBuilder.DropIndex(
                name: "IX_Job_JobStatusID",
                schema: "Request",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_SupportActivityID",
                schema: "Request",
                table: "Job");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Lookup");

            migrationBuilder.CreateTable(
                name: "JobStatus",
                schema: "Lookup",
                columns: table => new
                {
                    ID = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SupportActivity",
                schema: "Lookup",
                columns: table => new
                {
                    ID = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportActivity", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestJobStatus_JobStatusID",
                schema: "Request",
                table: "RequestJobStatus",
                column: "JobStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobStatusID",
                schema: "Request",
                table: "Job",
                column: "JobStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_SupportActivityID",
                schema: "Request",
                table: "Job",
                column: "SupportActivityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_JobStatusID",
                schema: "Request",
                table: "Job",
                column: "JobStatusID",
                principalSchema: "Lookup",
                principalTable: "JobStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportActivity_SupportActivityID",
                schema: "Request",
                table: "Job",
                column: "SupportActivityID",
                principalSchema: "Lookup",
                principalTable: "SupportActivity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobStatus_JobStatusID",
                schema: "Request",
                table: "RequestJobStatus",
                column: "JobStatusID",
                principalSchema: "Lookup",
                principalTable: "JobStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
