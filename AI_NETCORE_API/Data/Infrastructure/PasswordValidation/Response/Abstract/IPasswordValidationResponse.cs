using Data.Infrastructure.PasswordValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.PasswordValidation.Response.Abstract
{
    public interface IPasswordValidationResponse
    {
        PasswordValidationResultEnum PasswordValidationResult { get; }
    }
}
