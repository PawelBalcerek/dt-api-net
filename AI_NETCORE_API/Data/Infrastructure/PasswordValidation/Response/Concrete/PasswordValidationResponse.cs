using Data.Infrastructure.PasswordValidation.Response.Abstract;
using Data.Infrastructure.PasswordValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.PasswordValidation.Response.Concrete
{
    public class PasswordValidationResponse : IPasswordValidationResponse
    {
        public PasswordValidationResponse(PasswordValidationResultEnum passwordValidationResult)
        {
            PasswordValidationResult = passwordValidationResult;
        }

        public PasswordValidationResultEnum PasswordValidationResult { get; }
    }
}
