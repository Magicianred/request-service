using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestService.Repo.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RequestHelpFormVariant = HelpMyStreet.Utils.Enums.RequestHelpFormVariant;

namespace RequestService.Repo.Helpers
{
    public static class EnumRequestFormVariantExtensions
    {
        public static void SetEnumSupportActivityData(this EntityTypeBuilder<EnumRequestFormVariants> entity)
        {
            var variants = Enum.GetValues(typeof(RequestHelpFormVariant)).Cast<RequestHelpFormVariant>();

            foreach (var variant in variants)
            {
                entity.HasData(new EnumRequestFormVariants { Id = (int)variant, Name = variant.ToString() });
            }
        }
    }
}
