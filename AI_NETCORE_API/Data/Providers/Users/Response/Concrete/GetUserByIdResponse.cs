using Domain.BuisnessObject;
using Domain.Providers.Common.Enum;
using Domain.Providers.Users.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Response.Concrete
{
    public class GetUserByIdResponse : IGetUserByIdResponse
    {
        public GetUserByIdResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetUserByIdResponse(User user)
        {
            if (user == null)
            {
                ProvideResult = ProvideEnumResult.Exception;
            }
            else
            {
                User = user;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public User User { get; }

        public ProvideEnumResult ProvideResult { get; }
    }
}
