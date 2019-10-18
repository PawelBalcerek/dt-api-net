using Data.Providers.Common.Enum;
using Data.Providers.User.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.User.Response.Concrete
{
    public class GetUserByIdResponse : IGetUserByIdResponse
    {
        public GetUserByIdResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetUserByIdResponse(dynamic user)
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

        public dynamic User { get; }

        public ProvideEnumResult ProvideResult { get; }
    }
}
