using Data.Providers.Users.Request.Abstract;
using Data.Providers.Users.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Users.Abstract
{
    public interface IUserProvider
    {
        IGetUserByIdResponse GetUserById(IGetUserByIdRequest getUserByIdRequest);
    }
}
