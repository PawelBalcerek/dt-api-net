using Domain.Providers.Users.Request.Abstract;
using Domain.Providers.Users.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Abstract
{
    public interface IUserProvider
    {
        IGetUserByIdResponse GetUserById(IGetUserByIdRequest getUserByIdRequest);
    }
}
