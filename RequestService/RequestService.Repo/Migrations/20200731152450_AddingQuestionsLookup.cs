using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddingQuestionsLookup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                schema: "Lookup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "Question",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "SupportRequesting" },
                    { 2, "FaceMask_SpecificRequirements" },
                    { 3, "FaceMask_Amount" },
                    { 4, "FaceMask_Recipient" },
                    { 5, "FaceMask_Cost" },
                    { 6, "IsHealthCritical" },
                    { 7, "WillYouCompleteYourself" },
                    { 8, "FtlosDonationInformation" },
                    { 9, "CommunicationNeeds" },
                    { 10, "AnythingElseToTellUs" },
                    { 11, "AgeUKReference" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question",
                schema: "Lookup");
        }
    }
}
