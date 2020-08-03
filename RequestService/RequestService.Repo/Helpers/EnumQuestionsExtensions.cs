using HelpMyStreet.Utils.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestService.Repo.EntityFramework.Entities;
using System;
using System.Linq;

namespace RequestService.Repo.Helpers
{
    public static class EnumQuestionsExtensions
    {
        public static void SetEnumQuestionsData(this EntityTypeBuilder<EnumQuestions> entity)        
        {
            var questions = Enum.GetValues(typeof(Questions)).Cast<Questions>();

            foreach (var Questions in questions)
            {
                entity.HasData(new EnumQuestions { Id = (int)Questions, Name = Questions.ToString() });
            }
        }
    }
}
