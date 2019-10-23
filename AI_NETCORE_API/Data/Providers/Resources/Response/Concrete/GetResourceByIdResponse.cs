using Domain.BuisnessObject;
using Domain.Providers.Common.Enum;
using Domain.Providers.Resources.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Resources.Response.Concrete
{
    public class GetResourceByIdResponse : IGetResourceByIdResponse
    {
        public GetResourceByIdResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }
        public GetResourceByIdResponse(Resource resource)
        {
            if (NotFound(resource))
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                ProvideResult = ProvideEnumResult.Success;
                Resource = resource;
            }
        }

        private static bool NotFound(Resource resource)
        {
            return resource == null;
        }

        public Resource Resource { get; }

        public ProvideEnumResult ProvideResult { get; }
    }
}
