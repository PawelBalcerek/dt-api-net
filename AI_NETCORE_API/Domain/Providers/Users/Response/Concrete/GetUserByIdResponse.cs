using Domain.BusinessObject;
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

        public GetUserByIdResponse(User user, long dbTime)
        {
            if (user == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                User = user;
                ProvideResult = ProvideEnumResult.Success;
            }
            DbTime = dbTime;
        }

        public User User { get; }
        public long DbTime { get; }
        public ProvideEnumResult ProvideResult { get; }
    }
}
