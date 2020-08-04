using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class ReplaceKeysWithValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE [Request].[JobQuestions] SET Answer = 'Key workers' WHERE QuestionID = 4 AND Answer = 'keyworkers';
                UPDATE [Request].[JobQuestions] SET Answer = 'Someone helping key workers stay safe in their role (e.g. care home residents, visitors etc.)' WHERE QuestionID = 4 AND Answer = 'somonekeyworkers';
                UPDATE [Request].[JobQuestions] SET Answer = 'Members of the public' WHERE QuestionID = 4 AND Answer = 'memberspublic';

                UPDATE [Request].[JobQuestions] SET Answer = 'I can make a contribution' WHERE QuestionID = 5 AND Answer = 'Contribution';

                UPDATE [Request].[JobQuestions] SET Answer = 'Yes' WHERE QuestionID = 6 AND Answer = 'true';
                UPDATE [Request].[JobQuestions] SET Answer = 'No' WHERE QuestionID = 6 AND Answer = 'false';

                UPDATE [Request].[JobQuestions] SET Answer = 'Yes' WHERE QuestionID = 7 AND Answer = 'true';
                UPDATE [Request].[JobQuestions] SET Answer = 'No, please make it visible to other volunteers' WHERE QuestionID = 7 AND Answer = 'false';
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE [Request].[JobQuestions] SET Answer = 'keyworkers' WHERE QuestionID = 4 AND Answer = 'Key workers';
                UPDATE [Request].[JobQuestions] SET Answer = 'somonekeyworkers' WHERE QuestionID = 4 AND Answer = 'Someone helping key workers stay safe in their role (e.g. care home residents, visitors etc.)';
                UPDATE [Request].[JobQuestions] SET Answer = 'memberspublic' WHERE QuestionID = 4 AND Answer = 'Members of the public';

                UPDATE [Request].[JobQuestions] SET Answer = 'Contribution' WHERE QuestionID = 5 AND Answer = 'I can make a contribution';

                UPDATE [Request].[JobQuestions] SET Answer = 'true' WHERE QuestionID = 6 AND Answer = 'Yes';
                UPDATE [Request].[JobQuestions] SET Answer = 'false' WHERE QuestionID = 6 AND Answer = 'No';

                UPDATE [Request].[JobQuestions] SET Answer = 'true' WHERE QuestionID = 7 AND Answer = 'Yes';
                UPDATE [Request].[JobQuestions] SET Answer = 'false' WHERE QuestionID = 7 AND Answer = 'No, please make it visible to other volunteers';
            ");
        }
    }
}
