using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddRequestsAndTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Lookup");

            migrationBuilder.AddColumn<bool>(
                name: "ForRequestor",
                schema: "Request",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDetails",
                schema: "Request",
                table: "Request",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID_Recipient",
                schema: "Request",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID_Requester",
                schema: "Request",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReadPrivacyNotice",
                schema: "Request",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptedTerms",
                schema: "Request",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialCommunicationNeeds",
                schema: "Request",
                table: "Request",
                unicode: false,
                nullable: true);

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
                    Value = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportActivity", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "RequestPersonal",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    AddressLine1 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AddressLine3 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Locality = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Postcode = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    EmailAddress = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    MobilePhone = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    OtherPhone = table.Column<string>(unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                schema: "Request",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestId = table.Column<int>(nullable: false),
                    SupportActivityID = table.Column<byte>(nullable: false),
                    Details = table.Column<string>(unicode: false, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsHealthCritical = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NewRequest_NewRequestID",
                        column: x => x.RequestId,
                        principalSchema: "Request",
                        principalTable: "Request",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportActivity_SupportActivityID",
                        column: x => x.SupportActivityID,
                        principalSchema: "Lookup",
                        principalTable: "SupportActivity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestJobStatus",
                schema: "Request",
                columns: table => new
                {
                    JobID = table.Column<int>(nullable: false),
                    JobStatusID = table.Column<byte>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestJobStatus", x => new { x.JobID, x.DateCreated, x.JobStatusID });
                    table.ForeignKey(
                        name: "FK_Job_JobID",
                        column: x => x.JobID,
                        principalSchema: "Request",
                        principalTable: "Job",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobStatus_JobStatusID",
                        column: x => x.JobStatusID,
                        principalSchema: "Lookup",
                        principalTable: "JobStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_PersonID_Recipient",
                schema: "Request",
                table: "Request",
                column: "PersonID_Recipient");

            migrationBuilder.CreateIndex(
                name: "IX_Request_PersonID_Requester",
                schema: "Request",
                table: "Request",
                column: "PersonID_Requester");

            migrationBuilder.CreateIndex(
                name: "IX_Job_RequestId",
                schema: "Request",
                table: "Job",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_SupportActivityID",
                schema: "Request",
                table: "Job",
                column: "SupportActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestJobStatus_JobStatusID",
                schema: "Request",
                table: "RequestJobStatus",
                column: "JobStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestPersonal_Person_PersonID_Recipient",
                schema: "Request",
                table: "Request",
                column: "PersonID_Recipient",
                principalSchema: "RequestPersonal",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestPersonal_Person_PersonID_Requester",
                schema: "Request",
                table: "Request",
                column: "PersonID_Requester",
                principalSchema: "RequestPersonal",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestPersonal_Person_PersonID_Recipient",
                schema: "Request",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestPersonal_Person_PersonID_Requester",
                schema: "Request",
                table: "Request");

            migrationBuilder.DropTable(
                name: "RequestJobStatus",
                schema: "Request");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "RequestPersonal");

            migrationBuilder.DropTable(
                name: "Job",
                schema: "Request");

            migrationBuilder.DropTable(
                name: "JobStatus",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "SupportActivity",
                schema: "Lookup");

            migrationBuilder.DropIndex(
                name: "IX_Request_PersonID_Recipient",
                schema: "Request",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_PersonID_Requester",
                schema: "Request",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ForRequestor",
                schema: "Request",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "OtherDetails",
                schema: "Request",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "PersonID_Recipient",
                schema: "Request",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "PersonID_Requester",
                schema: "Request",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ReadPrivacyNotice",
                schema: "Request",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "AcceptedTerms",
                schema: "Request",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "SpecialCommunicationNeeds",
                schema: "Request",
                table: "Request");
        }
    }
}
