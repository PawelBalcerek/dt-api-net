using Data.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.User.Response.Abstract
{
    public interface IGetUserByIdResponse : IProvideResult
    {
        dynamic User { get; }
    }
}
