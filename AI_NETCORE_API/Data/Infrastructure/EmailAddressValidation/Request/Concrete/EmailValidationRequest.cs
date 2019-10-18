using Data.Infrastructure.EmailAddressValidation.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.EmailAddressValidation.Request.Concrete
{
    public class EmailValidationRequest : IEmailValidationRequest
    {
        public EmailValidationRequest(string emailToValidate)
        {
            EmailToValidate = emailToValidate;
        }

        public string EmailToValidate { get; }
    }
}
