using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class AddReferenceField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reference",
                schema: "Request",
                table: "Job",
                nullable: true);	
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
				name: "Reference",
				schema: "Request",
				table: "Job");
		}
    }
}
