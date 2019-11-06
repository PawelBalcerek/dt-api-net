using Domain.BusinessObject;
using Domain.Providers.Common.Abstract;
using Domain.Repositories.BaseRepo.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Resources.Response.Abstract
{
    public interface IGetResourcesResponse : IProvideResult, IDatabaseExecutionTimeDetails
    {
        IList<Resource> Resources { get; }
    }
}
