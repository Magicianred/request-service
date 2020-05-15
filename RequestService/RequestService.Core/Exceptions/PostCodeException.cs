using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Exceptions
{
    public class PostCodeException : Exception
    {
        public PostCodeException() : base("Invalid postcode")
        {
        }
    }
}
