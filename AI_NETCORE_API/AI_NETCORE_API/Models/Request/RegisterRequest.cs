using Data.Infrastructure.EmailAddressValidation.Abstract;
using Data.Infrastructure.EmailAddressValidation.Request.Abstract;
using Data.Infrastructure.EmailAddressValidation.Request.Concrete;
using Data.Infrastructure.EmailAddressValidation.Response.Abstract;
using Data.Infrastructure.EmailAddressValidation.Response.Enum;
using Data.Infrastructure.PasswordValidation.Abstract;
using Data.Infrastructure.PasswordValidation.Request.Abstract;
using Data.Infrastructure.PasswordValidation.Request.Concrete;
using Data.Infrastructure.PasswordValidation.Response.Abstract;
using Data.Infrastructure.PasswordValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Request
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public bool IsValid(IPasswordValidator passwordValidator, IEmailValidator emailValidator)
        {
            IPasswordValidationRequest passwordValidationRequest = new PasswordValidationRequest(Password);
            IPasswordValidationResponse passwordValidationResponse = passwordValidator.PasswordValidation(passwordValidationRequest);

            IEmailValidationRequest emailValidationRequest = new EmailValidationRequest(Email);
            IEmailValidationResponse emailValidationResponse = emailValidator.ValidateEmail(emailValidationRequest);

            return !string.IsNullOrWhiteSpace(Email) &&
                !string.IsNullOrWhiteSpace(Password) &&
                !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                !string.IsNullOrWhiteSpace(Name) &&
                ConfirmPassword == Password &&
                passwordValidationResponse.PasswordValidationResult == PasswordValidationResultEnum.Success &&
                emailValidationResponse.EmailValidationResult == EmailValidationResult.Success
            ;
        }
    }
}
