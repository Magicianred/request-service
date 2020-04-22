using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class GetChampionsByPostcodeResponse
    {
        public List<User> Users { get; set; }
    }
}
