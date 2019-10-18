using Data.Infrastructure.EmailAddressValidation.Request.Abstract;
using Data.Infrastructure.EmailAddressValidation.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.EmailAddressValidation.Abstract
{
    public interface IEmailValidator
    {
        IEmailValidationResponse ValidateEmail(IEmailValidationRequest emailValidationRequest);
    }
}
