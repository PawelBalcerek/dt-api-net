using Domain.Providers.Users.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Request.Concrete
{
    public class LoginUserRequest : ILoginUserRequest
    {
        public string Login { get; }

        public string Password { get; }

        public LoginUserRequest(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
