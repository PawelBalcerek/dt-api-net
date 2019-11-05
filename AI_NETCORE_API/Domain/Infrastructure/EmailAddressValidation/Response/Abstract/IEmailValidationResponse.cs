using Domain.Infrastructure.EmailAddressValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.EmailAddressValidation.Response.Abstract
{
    public interface IEmailValidationResponse
    {
        EmailValidationResult EmailValidationResult { get; } 
    }
}
