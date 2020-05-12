using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddCreatedByUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "Request",
                table: "RequestJobStatus",
                newName: "VolunteerUserID");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserID",
                schema: "Request",
                table: "RequestJobStatus",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "JobStatusID",
                schema: "Request",
                table: "Job",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolunteerUserID",
                schema: "Request",
                table: "Job",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobStatusID",
                schema: "Request",
                table: "Job",
                column: "JobStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_JobStatusID",
                schema: "Request",
                table: "Job",
                column: "JobStatusID",
                principalSchema: "Lookup",
                principalTable: "JobStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_JobStatusID",
                schema: "Request",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_JobStatusID",
                schema: "Request",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "Request",
                table: "RequestJobStatus");

            migrationBuilder.DropColumn(
                name: "JobStatusID",
                schema: "Request",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "VolunteerUserID",
                schema: "Request",
                table: "Job");

            migrationBuilder.RenameColumn(
                name: "VolunteerUserID",
                schema: "Request",
                table: "RequestJobStatus",
                newName: "UserID");
        }
    }
}
