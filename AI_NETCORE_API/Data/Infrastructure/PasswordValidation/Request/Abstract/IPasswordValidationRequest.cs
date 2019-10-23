using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.PasswordValidation.Request.Abstract
{
    public interface IPasswordValidationRequest
    {
        string PasswordToValidate { get; }
    }
}
