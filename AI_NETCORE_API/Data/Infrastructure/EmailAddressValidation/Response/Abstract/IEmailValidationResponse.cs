using Data.Infrastructure.EmailAddressValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.EmailAddressValidation.Response.Abstract
{
    public interface IEmailValidationResponse
    {
        EmailValidationResult EmailValidationResult { get; } 
    }
}
