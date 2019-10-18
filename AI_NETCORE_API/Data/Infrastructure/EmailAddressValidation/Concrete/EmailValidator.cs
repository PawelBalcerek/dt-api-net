using Data.Infrastructure.EmailAddressValidation.Abstract;
using Data.Infrastructure.EmailAddressValidation.Request.Abstract;
using Data.Infrastructure.EmailAddressValidation.Response.Abstract;
using Data.Infrastructure.EmailAddressValidation.Response.Concrete;
using Data.Infrastructure.EmailAddressValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.EmailAddressValidation.Concrete
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
