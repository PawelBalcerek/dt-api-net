using Domain.Providers.Users.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Response.Concrete
{
    public class LoginUserResponse : ILoginUserResponse
    {
        public string Token { get; }
        public long DbTime { get; }
        public LoginUserResponse(string token, long dbTime)
        {
            Token = token;
            DbTime = dbTime;
        }
    }
}
