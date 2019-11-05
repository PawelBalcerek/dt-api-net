using Domain.Infrastructure.PasswordValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.PasswordValidation.Response.Abstract
{
    public interface IPasswordValidationResponse
    {
        PasswordValidationResultEnum PasswordValidationResult { get; }
    }
}
