using Data.Infrastructure.PasswordValidation.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.PasswordValidation.Request.Concrete
{
    public class PasswordValidationRequest : IPasswordValidationRequest
    {
        public PasswordValidationRequest(string passwortToValidate)
        {
            PasswortToValidate = passwortToValidate;
        }

        public string PasswortToValidate { get; }
    }
}
