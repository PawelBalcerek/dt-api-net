using Domain.Infrastructure.EmailAddressValidation.Response.Abstract;
using Domain.Infrastructure.EmailAddressValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.EmailAddressValidation.Response.Concrete
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
