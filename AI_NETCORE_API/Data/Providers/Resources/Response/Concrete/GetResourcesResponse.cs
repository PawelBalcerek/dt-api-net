using Data.BuisnessObject;
using Data.Providers.Common.Enum;
using Data.Providers.Resources.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Resources.Response.Concrete
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
