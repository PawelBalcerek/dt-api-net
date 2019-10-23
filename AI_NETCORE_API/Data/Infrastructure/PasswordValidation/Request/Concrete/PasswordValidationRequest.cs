using Domain.Infrastructure.PasswordValidation.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.PasswordValidation.Request.Concrete
{
    public class PasswordValidationRequest : IPasswordValidationRequest
    {
        public PasswordValidationRequest(string passwortToValidate)
        {
            PasswordToValidate = passwortToValidate;
        }

        public string PasswordToValidate { get; }
    }
}
