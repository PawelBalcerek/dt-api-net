using Data.Infrastructure.EmailAddressValidation.Response.Abstract;
using Data.Infrastructure.EmailAddressValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.EmailAddressValidation.Response.Concrete
{
    public class EmailValidationResponse : IEmailValidationResponse
    {
        public EmailValidationResponse(EmailValidationResult emailValidationResult)
        {
            EmailValidationResult = emailValidationResult;
        }

        public EmailValidationResult EmailValidationResult { get; }
    }
}
