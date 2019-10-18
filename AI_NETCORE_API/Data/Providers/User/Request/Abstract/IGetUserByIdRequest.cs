using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.User.Request.Abstract
{
    public interface IGetUserByIdRequest
    {
        int Id { get; }
    }
}
