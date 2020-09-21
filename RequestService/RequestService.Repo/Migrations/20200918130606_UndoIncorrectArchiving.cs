using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class UndoIncorrectArchiving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                            update Request.Request Set Archive=null
                            from Request.Request r
                            inner join Request.Job j on r.ID = j.RequestId
                            where Archive = 1 and j.JobStatusID in (1,2)
                        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
