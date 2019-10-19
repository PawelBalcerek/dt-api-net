using Data.Providers.Resources.Request.Abstract;
using Data.Providers.Resources.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Resources.Abstract
{
    public interface IResourcesProvider
    {
        IGetResourceByIdResponse GetResourceById(IGetResourceByIdRequest getResourceByIdRequest);
        IGetResourcesResponse GetResources();
    }
}
