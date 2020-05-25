using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class GetHelpersByPostcodeAndTaskTypeResponse
    {
        public List<HelperContactInformation> Users { get; set; }
    }

    public class HelperContactInformation
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool IsStreetChampionOfPostcode { get; set; }
        public bool IsVerified { get; set; }
        public List<SupportActivities> SupportedActivites { get; set; }

    }

}

