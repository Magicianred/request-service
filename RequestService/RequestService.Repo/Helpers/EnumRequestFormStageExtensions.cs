using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestService.Repo.EntityFramework.Entities;
using System;
using System.Linq;
using RequestHelpFormStage = HelpMyStreet.Utils.Enums.RequestHelpFormStage;

namespace RequestService.Repo.Helpers
{
    public static class EnumRequestFormStageExtensions
    {
        public static void SetEnumRequestFormStagesData(this EntityTypeBuilder<EnumRequestFormStages> entity)
        {
            var variants = Enum.GetValues(typeof(RequestHelpFormStage)).Cast<RequestHelpFormStage>();

            foreach (var variant in variants)
            {
                entity.HasData(new EnumRequestFormStages { Id = (int)variant, Name = variant.ToString() });
            }
        }
    }
}
