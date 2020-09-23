using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class BackPopulateReferenceFieldInJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                            update Request.Job
            Set Reference = Answer
            from Request.Job j
            inner join Request.JobQuestions jq on j.ID = jq.JobID
            where jq.QuestionID = 11                
                        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                            update Request.Job Set Reference=null where Reference!=null                            
                        ");
        }
    }
}
