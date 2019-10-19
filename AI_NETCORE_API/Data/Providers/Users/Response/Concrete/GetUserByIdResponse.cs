using Data.BuisnessObject;
using Data.Providers.Common.Enum;
using Data.Providers.Users.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Users.Response.Concrete
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
