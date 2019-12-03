using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Repositories.UserRepo.Const;

namespace Domain.Creators.Users.Response.Abstract
{
    public interface IUserCreateResponse
    {
        CreateUserResponseEnum ResponseEnum { get; }
        long DbTime { get; }
    }
}
