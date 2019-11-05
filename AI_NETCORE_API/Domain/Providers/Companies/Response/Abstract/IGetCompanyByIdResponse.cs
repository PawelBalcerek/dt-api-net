using Domain.BusinessObject;
using Domain.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Response.Abstract;

namespace Domain.Providers.Companies.Response.Abstract
{
    public interface IGetCompanyByIdResponse : IProvideResult, IDatabaseExecutionTimeDetails
    {
        Company Company { get; }
    }
}
