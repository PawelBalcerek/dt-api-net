using Domain.Infrastructure.EmailAddressValidation.Abstract;
using Domain.Infrastructure.EmailAddressValidation.Request.Abstract;
using Domain.Infrastructure.EmailAddressValidation.Response.Abstract;
using Domain.Infrastructure.EmailAddressValidation.Response.Concrete;
using Domain.Infrastructure.EmailAddressValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.EmailAddressValidation.Concrete
{
    public class EmailValidator : IEmailValidator
    {
        public IEmailValidationResponse ValidateEmail(IEmailValidationRequest emailValidationRequest)
        {
            try
            {
                System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(emailValidationRequest.EmailToValidate);
                return new EmailValidationResponse(addr.Address == emailValidationRequest.EmailToValidate? EmailValidationResult.Success:EmailValidationResult.InvalidEmail);
            }
            catch
            {
                return new EmailValidationResponse(EmailValidationResult.Exception);
            }
        }
    }
}
