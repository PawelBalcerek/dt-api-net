using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Request.Abstract
{
    public interface ILoginUserRequest
    {
        string Login { get; }
        string Password { get; }
    }
}
