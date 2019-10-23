using Domain.Infrastructure.EmailAddressValidation.Abstract;
using Domain.Infrastructure.EmailAddressValidation.Request.Abstract;
using Domain.Infrastructure.EmailAddressValidation.Request.Concrete;
using Domain.Infrastructure.EmailAddressValidation.Response.Abstract;
using Domain.Infrastructure.EmailAddressValidation.Response.Enum;
using Domain.Infrastructure.PasswordValidation.Abstract;
using Domain.Infrastructure.PasswordValidation.Request.Abstract;
using Domain.Infrastructure.PasswordValidation.Request.Concrete;
using Domain.Infrastructure.PasswordValidation.Response.Abstract;
using Domain.Infrastructure.PasswordValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Request
{

    public class RegisterRequest
    {
        [Required]
        
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
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
