using Data.Providers.User.Request.Abstract;
using Data.Providers.User.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.User.Abstract
{
    public interface IUserProvider
    {
        IGetUserByIdResponse GetUserById(IGetUserByIdRequest getUserByIdRequest);
    }
}
