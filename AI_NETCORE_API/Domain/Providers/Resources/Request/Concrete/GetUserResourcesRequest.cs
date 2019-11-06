using Domain.Providers.Resources.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Resources.Request.Concrete
{
    public class GetUserResourcesRequest : IGetUserResourcesRequest
    {
        public GetUserResourcesRequest(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}
