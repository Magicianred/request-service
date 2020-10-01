using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class MakeReferringGroupIDMandatory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReferringGroupId",
                schema: "Request",
                table: "Request",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReferringGroupId",
                schema: "Request",
                table: "Request",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
