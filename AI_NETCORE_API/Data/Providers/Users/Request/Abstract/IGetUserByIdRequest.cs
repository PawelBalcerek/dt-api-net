using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Users.Request.Abstract
{
    public interface IGetUserByIdRequest
    {
        int Id { get; }
    }
}
