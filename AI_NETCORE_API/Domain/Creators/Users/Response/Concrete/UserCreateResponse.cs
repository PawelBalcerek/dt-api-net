using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Creators.Users.Response.Abstract;

namespace Domain.Creators.Users.Response.Concrete
{
    public class UserCreateResponse : IUserCreateResponse
    {
        public UserCreateResponse(bool success, long dbTime)
        {
            Success = success;
            DbTime = dbTime;
        }

        public UserCreateResponse()
        {
            Success = false;
            DbTime = 0;
        }

        public bool Success { get; }
        public long DbTime { get; }
    }
}
