using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddActivityEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Lookup");

            migrationBuilder.CreateTable(
                name: "SupportActivity",
                schema: "Lookup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportActivity", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "SupportActivity",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Shopping" },
                    { 2, "CollectingPrescriptions" },
                    { 3, "Errands" },
                    { 4, "MedicalAppointmentTransport" },
                    { 5, "DogWalking" },
                    { 6, "MealPreparation" },
                    { 7, "PhoneCalls_Friendly" },
                    { 8, "PhoneCalls_Anxious" },
                    { 9, "HomeworkSupport" },
                    { 10, "CheckingIn" },
                    { 11, "Other" },
                    { 12, "FaceMask" },
                    { 13, "WellbeingPackage" },
                    { 14, "CommunityConnector" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportActivity",
                schema: "Lookup");
        }
    }
}
