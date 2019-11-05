using Domain.Providers.Users.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Response.Concrete
{
    public class LoginUserResponse : ILoginUserResponse
    {
        public string Token { get; }

        public LoginUserResponse(string token)
        {
            Token = token;
        }
    }
}
