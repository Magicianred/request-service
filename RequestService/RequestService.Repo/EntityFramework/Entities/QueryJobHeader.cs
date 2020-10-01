using System;

namespace RequestService.Repo.EntityFramework.Entities
{
    public class QueryJobHeader
    {
        public int? VolunteerUserID { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateStatusLastChanged { get; set; }
        public int ReferringGroupID { get; set; }
        public double DistanceInMiles { get; set; }
        public string PostCode { get; set; }
        public bool IsHealthCritical { get; set; }
        public DateTime DueDate { get; set; }
        public byte SupportActivityID { get; set; }
        public byte? JobStatusID { get; set; }
        public int JobID { get; set; }
        public bool? Archive { get; set; }
        public string Reference { get; set; }
    }
}
