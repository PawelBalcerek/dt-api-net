using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Request.Abstract
{
    public interface IGetUserByIdRequest
    {
        int Id { get; }
    }
}
