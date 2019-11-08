using Domain.Providers.Users.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Request.Concrete
{
    public class LoginUserRequest : ILoginUserRequest
    {
        public string Email { get; }

        public string Password { get; }

        public LoginUserRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
