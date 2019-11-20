using Domain.BusinessObject;
using Domain.Providers.Common.Abstract;
using Domain.Repositories.BaseRepo.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Configurations.Response.Abstract
{
    public interface IGetConfigurationResponse : IProvideResult, IDatabaseExecutionTimeDetails
    {
        Configuration Configuration { get; }
    }
}
