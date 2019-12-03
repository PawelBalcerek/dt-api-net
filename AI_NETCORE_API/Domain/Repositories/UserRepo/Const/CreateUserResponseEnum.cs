using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories.UserRepo.Const
{
    public enum CreateUserResponseEnum
    {
        Exception = -1,
        Success = 0,
        EmailAlreadyExists = 1
    }
}
