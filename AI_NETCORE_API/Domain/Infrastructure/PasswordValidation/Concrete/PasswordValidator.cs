using Domain.Infrastructure.AppsettingsConfiguration.Abstract;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Infrastructure.PasswordValidation.Abstract;
using Domain.Infrastructure.PasswordValidation.Request.Abstract;
using Domain.Infrastructure.PasswordValidation.Response.Abstract;
using Domain.Infrastructure.PasswordValidation.Response.Concrete;
using Domain.Infrastructure.PasswordValidation.Response.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Infrastructure.PasswordValidation.Concrete
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
                    regex.IsMatch(passwordValidationRequest.PasswordToValidate) ?
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
