using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class RequestorDefinedByGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequestorDefinedByGroup",
                schema: "Request",
                table: "Request",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql(@"
                            update Request.Request Set RequestorDefinedByGroup=1
                            from Request.Request r
                            where ReferringGroupId = -7");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestorDefinedByGroup",
                schema: "Request",
                table: "Request");
        }
    }
}
