using System;
using System.Collections.Generic;
using System.Text;
using Domain.Creators.Users.Request.Abstract;

namespace Domain.Creators.Users.Request.Concrete
{
    public class UserCreateRequest : IUserCreateRequest
    {
        public UserCreateRequest(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
        }

        public string UserName { get; }
        public string Password { get; }
        public string Email { get; }
    }
}
