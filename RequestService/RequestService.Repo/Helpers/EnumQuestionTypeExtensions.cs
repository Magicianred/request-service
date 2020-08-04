using HelpMyStreet.Utils.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestService.Repo.EntityFramework.Entities;
using System;
using System.Linq;

namespace RequestService.Repo.Helpers
{
    public static class EnumQuestionTypeExtensions
    {
        public static void SetEnumQuestionTypeData(this EntityTypeBuilder<EnumQuestionTypes> entity)        
        {
            var questionTypes = Enum.GetValues(typeof(QuestionType)).Cast<QuestionType>();

            foreach (var questionType in questionTypes)
            {
                entity.HasData(new EnumQuestionTypes { Id = (int)questionType, Name = questionType.ToString() });
            }
        }
    }
}
