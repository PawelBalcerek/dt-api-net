using Domain.BuisnessObject;
using Domain.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Resources.Response.Abstract
{
    public interface IGetResourcesResponse : IProvideResult
    {
        IList<Resource> Resources { get; }
    }
}
