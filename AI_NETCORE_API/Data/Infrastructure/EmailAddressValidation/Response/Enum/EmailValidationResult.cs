using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.EmailAddressValidation.Response.Enum
{
    public enum EmailValidationResult
    {
        Exception = -1,
        Success = 0,
        InvalidEmail = 1
    }
}
