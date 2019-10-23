using System;
using System.Collections.Generic;
using System.Text;
using Domain.Creators.Users.Request.Abstract;
using Domain.Creators.Users.Response.Abstract;

namespace Domain.Creators.Users.Abstract
{
    public interface IUserCreator
    {
        IUserCreateResponse CreateUser(IUserCreateRequest userCreateRequest);
    }
}
