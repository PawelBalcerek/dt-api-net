using Domain.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Response.Abstract
{
    public interface IGetUserByIdResponse : IProvideResult
    {
        BusinessObject.User User { get; }
        long DbTime { get; }
    }
}
