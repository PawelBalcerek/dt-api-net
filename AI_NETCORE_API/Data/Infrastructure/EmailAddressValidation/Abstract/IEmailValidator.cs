using Domain.Infrastructure.EmailAddressValidation.Request.Abstract;
using Domain.Infrastructure.EmailAddressValidation.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.EmailAddressValidation.Abstract
{
    public interface IEmailValidator
    {
        IEmailValidationResponse ValidateEmail(IEmailValidationRequest emailValidationRequest);
    }
}
