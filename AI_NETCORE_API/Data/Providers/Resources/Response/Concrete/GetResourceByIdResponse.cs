using Data.BuisnessObject;
using Data.Providers.Common.Enum;
using Data.Providers.Resources.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Resources.Response.Concrete
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
