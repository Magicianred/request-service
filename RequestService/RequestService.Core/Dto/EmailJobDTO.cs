using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class EmailJobDTO
    {

        public bool IsHealthCritical { get; set; }
        public string PostCode { get; set; }
        public SupportActivities Activity { get; set; }
        public string DueDate { get; set; }
        public HelpMyStreet.Utils.Models.RequestPersonalDetails Requestor { get; set; }
        public bool IsStreetChampionOfPostcode { get; set; }
        public bool IsVerified { get; set; }
        public double DistanceFromPostcode { get; set; }
        public string EncodedJobID { get; set; }
  

        public static EmailJobDTO GetEmailJobDTO(PostNewRequestForHelpRequest request, HelpMyStreet.Utils.Models.Job job, string postCode)
        {
            return new EmailJobDTO()
            {
                IsHealthCritical = job.HealthCritical,
                PostCode = postCode,
                Activity = job.SupportActivity,
                Requestor = request.HelpRequest.Requestor,
                DueDate = DateTime.Now.AddDays(job.DueDays).ToString("dd/MM/yyyy"),
                EncodedJobID = HelpMyStreet.Utils.Utils.Base64Utils.Base64Encode(job.JobID.ToString())
               
            };
        }
    }

}
