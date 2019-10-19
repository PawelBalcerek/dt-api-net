using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Resources.Request.Abstract
{
    public interface IGetResourceByIdRequest
    {
        int ResourceId { get; }
    }
}
