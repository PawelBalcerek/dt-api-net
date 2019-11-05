using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Creators.Users.Response.Abstract;

namespace Domain.Creators.Users.Response.Concrete
{
    public class UserCreateResponse : IUserCreateResponse
    {
        public UserCreateResponse(bool success)
        {
            Success = success;
        }

        public UserCreateResponse()
        {
            Success = false;
        }

        public bool Success { get; }
    }
}
