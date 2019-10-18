using Data.Infrastructure.PasswordValidation.Request.Abstract;
using Data.Infrastructure.PasswordValidation.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.PasswordValidation.Abstract
{
    public interface IPasswordValidator
    {
        IPasswordValidationResponse PasswordValidation(IPasswordValidationRequest passwordValidationRequest);
    }
}
