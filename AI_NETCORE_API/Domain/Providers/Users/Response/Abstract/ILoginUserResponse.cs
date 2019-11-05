using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Response.Abstract
{
    public interface ILoginUserResponse
    {
        string Token { get; }
    }
}
