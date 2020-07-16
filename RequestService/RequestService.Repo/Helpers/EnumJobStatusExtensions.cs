
using HelpMyStreet.Utils.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestService.Repo.EntityFramework.Entities;
using System;
using System.Linq;
using SupportActivities = HelpMyStreet.Utils.Enums.SupportActivities;

namespace RequestService.Repo.Helpers
{
    public static class EnumJobStatusExtensions
    {
        public static void SetEnumJobStatusData(this EntityTypeBuilder<EnumJobStatuses> entity)        
        {
            var jobStatuses = Enum.GetValues(typeof(JobStatuses)).Cast<JobStatuses>();

            foreach (var jobStatus in jobStatuses)
            {
                entity.HasData(new EnumJobStatuses { Id = (int)jobStatus, Name = jobStatus.ToString() });
            }
        }
    }
}
