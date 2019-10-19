using Data.Providers.Resources.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Resources.Request.Concrete
{
    public class GetResourceByIdRequest : IGetResourceByIdRequest
    {
        public GetResourceByIdRequest(int resourceId)
        {
            ResourceId = resourceId;
        }

        public int ResourceId { get; }
    }
}
