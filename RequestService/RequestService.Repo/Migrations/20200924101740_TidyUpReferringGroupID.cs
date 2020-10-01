using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class TidyUpReferringGroupID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                            update Request.Request Set ReferringGroupId=-4
                            from Request.Request r
                            inner join Request.Job j on r.ID = j.RequestId
                            where ReferringGroupId is null and j.SupportActivityID=14
                        ");

            migrationBuilder.Sql(@"
                            update Request.Request Set ReferringGroupId=-3
                            from Request.Request r
                            inner join Request.Job j on r.ID = j.RequestId
                            where ReferringGroupId is null and j.SupportActivityID=13
                        ");

            migrationBuilder.Sql(@"
                            update Request.Request 
                            Set ReferringGroupId=-1
                            where ReferringGroupId is null
                        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}