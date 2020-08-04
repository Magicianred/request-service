using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class RequestHelpFormStageLookup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestFormStage",
                schema: "Lookup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestFormStage", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "RequestFormStage",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Request" });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "RequestFormStage",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "Detail" });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "RequestFormStage",
                columns: new[] { "ID", "Name" },
                values: new object[] { 3, "Review" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestFormStage",
                schema: "Lookup");
        }
    }
}
