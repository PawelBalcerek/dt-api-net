using Domain.Infrastructure.PasswordValidation.Response.Abstract;
using Domain.Infrastructure.PasswordValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.PasswordValidation.Response.Concrete
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
