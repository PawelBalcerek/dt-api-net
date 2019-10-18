using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.EmailAddressValidation.Response.Enum
{
    public enum EmailValidationResult
    {
        Exception = -1,
        Success = 0,
        InvalidEmail = 1
    }
}
