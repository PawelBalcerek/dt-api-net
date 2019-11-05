using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;

namespace Domain.Creators.Users.Response.Abstract
{
    public interface IUserCreateResponse
    {
        bool Success { get; }
    }
}
