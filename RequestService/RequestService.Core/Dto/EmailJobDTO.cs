using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class EmailJobDTO
    {
        public bool OnBehalfOfSomeone { get; set; }
        public bool IsHealthCritical { get; set; }
        public string PostCode { get; set; }
        public SupportActivities Activity { get; set; }
        public string FurtherDetails { get; set; }
        public string SpecialCommunicationNeeds { get; set; }
        public string OtherDetails { get; set; }
        public string DueDate { get; set; }
        public HelpMyStreet.Utils.Models.RequestPersonalDetails Requestor { get; set; }

        public static EmailJobDTO GetEmailJobDTO(PostNewRequestForHelpRequest request, HelpMyStreet.Utils.Models.Job job, string postCode)
        {
            return new EmailJobDTO()
            {
                OnBehalfOfSomeone = !request.HelpRequest.ForRequestor,
                IsHealthCritical = job.HealthCritical,
                OtherDetails = job.Details,
                PostCode = postCode,
                Activity = job.SupportActivity,
                Requestor = request.HelpRequest.Requestor,
                SpecialCommunicationNeeds = request.HelpRequest.SpecialCommunicationNeeds,
                FurtherDetails = request.HelpRequest.OtherDetails,
                DueDate = DateTime.Now.AddDays(job.DueDays).ToString("dd/MM/yyyy")
            };
        }
    }

}
