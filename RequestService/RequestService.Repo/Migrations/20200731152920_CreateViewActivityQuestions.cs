using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class CreateViewActivityQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

			CREATE VIEW [QuestionSet].[viewActivityQuestions]
			AS
			SELECT	RequestFormVariant.Name as RequestFormVariant,
					SupportActivity.Name as SupportActivity,
					RequestFormStage.Name as RequestFormStage,
					ActivityQuestions.QuestionID,
					QuestionLookup.Name as Question,
					Question.Name as Label,
					QuestionType.Name as QuestionType,
					Question.AdditionalData,
					ActivityQuestions.Location,
					ActivityQuestions.[Order],
					ActivityQuestions.Required,
					ActivityQuestions.PlaceholderText,
					ActivityQuestions.Subtext
			FROM	QuestionSet.ActivityQuestions
			INNER JOIN QuestionSet.Question
					ON ActivityQuestions.QuestionID = Question.ID
			INNER JOIN Lookup.SupportActivity
					ON ActivityQuestions.ActivityID = SupportActivity.ID
			INNER JOIN Lookup.RequestFormVariant
					ON ActivityQuestions.RequestFormVariantID = RequestFormVariant.ID
			INNER JOIN Lookup.Question AS QuestionLookup
					ON ActivityQuestions.QuestionID = Question.ID
			INNER JOIN Lookup.QuestionType
					ON Question.QuestionType = QuestionType.ID
			INNER JOIN Lookup.RequestFormStage
					ON ActivityQuestions.RequestFormStageID = RequestFormStage.ID
			
			");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"

			DROP VIEW [QuestionSet].[viewActivityQuestions]
			
			");
		}
	}
}
