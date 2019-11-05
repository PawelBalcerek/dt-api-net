using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.Users.Request.Abstract
{
    public interface IUserCreateRequest
    {
        string UserName { get; }
        string Password { get; }
        string Email { get; }
    }
}
