using Domain.Providers.Resources.Request.Abstract;
using Domain.Providers.Resources.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Resources.Abstract
{
    public interface IResourcesProvider
    {
        IGetResourcesResponse GetUserResources(IGetUserResourcesRequest getUserResourcesRequest);
    }
}
