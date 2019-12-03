using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Creators.Users.Response.Abstract;
using Domain.Repositories.UserRepo.Const;

namespace Domain.Creators.Users.Response.Concrete
{
    public class UserCreateResponse : IUserCreateResponse
    {
        public UserCreateResponse(CreateUserResponseEnum responseEnum, long dbTime)
        {
            ResponseEnum = responseEnum;
            DbTime = dbTime;
        }

        public UserCreateResponse()
        {
            ResponseEnum = CreateUserResponseEnum.Exception;
            DbTime = 0;
        }

        public CreateUserResponseEnum ResponseEnum { get; }
        public long DbTime { get; }
    }
}
