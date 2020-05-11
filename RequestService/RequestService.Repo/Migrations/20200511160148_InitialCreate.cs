using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RequestPersonal");

            migrationBuilder.EnsureSchema(
                name: "Request");

            migrationBuilder.CreateTable(
                name: "Request",
                schema: "Request",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostCode = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    IsFulfillable = table.Column<bool>(nullable: false),
                    CommunicationSent = table.Column<bool>(nullable: false),
                    FulfillableStatus = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SupportActivities",
                schema: "Request",
                columns: table => new
                {
                    RequestID = table.Column<int>(nullable: false),
                    ActivityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportActivities", x => new { x.RequestID, x.ActivityID });
                    table.ForeignKey(
                        name: "FK_SupportActivities_RequestID",
                        column: x => x.RequestID,
                        principalSchema: "Request",
                        principalTable: "Request",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDetails",
                schema: "RequestPersonal",
                columns: table => new
                {
                    RequestID = table.Column<int>(nullable: false),
                    OnBehalfOfAnother = table.Column<bool>(nullable: false),
                    FurtherDetails = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    RequestorFirstName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    RequestorLastName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    RequestorEmailAddress = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    RequestorPhoneNumber = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDetails", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_PersonalDetails_RequestID",
                        column: x => x.RequestID,
                        principalSchema: "Request",
                        principalTable: "Request",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportActivities",
                schema: "Request");

            migrationBuilder.DropTable(
                name: "PersonalDetails",
                schema: "RequestPersonal");

            migrationBuilder.DropTable(
                name: "Request",
                schema: "Request");
        }
    }
}
