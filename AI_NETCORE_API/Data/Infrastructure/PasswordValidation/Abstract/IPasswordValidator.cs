using Domain.Infrastructure.PasswordValidation.Request.Abstract;
using Domain.Infrastructure.PasswordValidation.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.PasswordValidation.Abstract
{
    public interface IPasswordValidator
    {
        IPasswordValidationResponse PasswordValidation(IPasswordValidationRequest passwordValidationRequest);
    }
}
