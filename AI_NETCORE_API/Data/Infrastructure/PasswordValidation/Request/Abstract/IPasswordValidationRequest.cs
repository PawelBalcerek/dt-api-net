using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.PasswordValidation.Request.Abstract
{
    public interface IPasswordValidationRequest
    {
        string PasswortToValidate { get; }
    }
}
