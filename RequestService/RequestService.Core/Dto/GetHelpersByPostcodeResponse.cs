using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class GetHelpersByPostcodeResponse
    {
        public List<User> Users { get; set; }
    }
}
