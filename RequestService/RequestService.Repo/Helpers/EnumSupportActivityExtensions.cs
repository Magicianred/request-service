
using HelpMyStreet.Utils.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestService.Repo.EntityFramework.Entities;
using System;
using System.Linq;
using SupportActivities = HelpMyStreet.Utils.Enums.SupportActivities;

namespace RequestService.Repo.Helpers
{
    public static class EnumSupportActivityExtensions
    {
        public static void SetEnumSupportActivityData(this EntityTypeBuilder<EnumSupportActivities> entity)        
        {
            var activites = Enum.GetValues(typeof(SupportActivities)).Cast<SupportActivities>();

            foreach (var activity in activites)
            {
                entity.HasData(new EnumSupportActivities { Id = (int)activity, Name = activity.ToString() });
            }
        }
    }
}
