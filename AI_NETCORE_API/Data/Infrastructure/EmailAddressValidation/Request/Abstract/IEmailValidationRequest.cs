using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.EmailAddressValidation.Request.Abstract
{
    public interface IEmailValidationRequest
    {
        string EmailToValidate { get; }
    }
}
