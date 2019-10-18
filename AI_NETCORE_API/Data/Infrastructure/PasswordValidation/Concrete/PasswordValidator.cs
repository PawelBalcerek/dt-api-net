using Data.Infrastructure.AppsettingsConfiguration.Abstract;
using Data.Infrastructure.Logging.Abstract;
using Data.Infrastructure.PasswordValidation.Abstract;
using Data.Infrastructure.PasswordValidation.Request.Abstract;
using Data.Infrastructure.PasswordValidation.Response.Abstract;
using Data.Infrastructure.PasswordValidation.Response.Concrete;
using Data.Infrastructure.PasswordValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Data.Infrastructure.PasswordValidation.Concrete
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly ILogger _logger;
        private readonly IAppsettingsProvider _appsettingsProvider;

        public PasswordValidator(ILogger logger, IAppsettingsProvider appsettingsProvider)
        {
            _logger = logger;
            _appsettingsProvider = appsettingsProvider;
        }

        public IPasswordValidationResponse PasswordValidation(IPasswordValidationRequest passwordValidationRequest)
        {
            try
            {
                Regex regex = new Regex(_appsettingsProvider.GetPasswordRegex());
                return new PasswordValidationResponse(
                    regex.IsMatch(passwordValidationRequest.PasswortToValidate) ?
                    PasswordValidationResultEnum.Success :
                    PasswordValidationResultEnum.InvalidInput);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new PasswordValidationResponse(PasswordValidationResultEnum.Exception);
            }
        }
    }
}
