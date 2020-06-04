using System;
using System.Collections.Generic;
using System.Text;
using HelpMyStreet.Utils.Enums;
namespace RequestService.Core.Dto
{
    public class OpenJobRequestDTO
    {        
           public SupportActivities SupportActivity { get; set; }
           public bool IsCritical { get; set; }
           public DateTime DueDate { get; set; }
           public double Distance { get; set; }
           public string Postcode { get; set; }
           public string EncodedJobID { get; set; }    
    }

}
