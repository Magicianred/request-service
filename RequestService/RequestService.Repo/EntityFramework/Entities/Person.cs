using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Repo.EntityFramework.Entities
{
    public class Person
    {
        public Person()
        {
            RequestPersonIdRecipientNavigation = new HashSet<Request>();
            RequestPersonIdRequesterNavigation = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Locality { get; set; }
        public string Postcode { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public string OtherPhone { get; set; }

        public virtual ICollection<Request> RequestPersonIdRecipientNavigation { get; set; }
        public virtual ICollection<Request> RequestPersonIdRequesterNavigation { get; set; }
    }
}
