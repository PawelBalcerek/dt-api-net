using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Resources.Request.Abstract
{
    public interface IGetUserResourcesRequest
    {
        int UserId { get; }
    }
}
