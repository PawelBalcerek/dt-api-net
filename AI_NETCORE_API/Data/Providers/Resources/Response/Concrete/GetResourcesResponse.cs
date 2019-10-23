using Domain.BuisnessObject;
using Domain.Providers.Common.Enum;
using Domain.Providers.Resources.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Resources.Response.Concrete
{
    public class GetResourcesResponse : IGetResourcesResponse
    {
        public GetResourcesResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetResourcesResponse(IList<Resource> resources)
        {
            if (resources != null)
            {
                Resources = resources;
                ProvideResult = ProvideEnumResult.Success;
            }
            else
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
        }

        public IList<Resource> Resources { get; }

        public ProvideEnumResult ProvideResult { get; }
    }
}
