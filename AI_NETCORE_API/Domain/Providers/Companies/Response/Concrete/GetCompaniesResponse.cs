using Domain.BusinessObject;
using Domain.Providers.Common.Enum;
using Domain.Providers.Companies.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Companies.Response.Concrete
{
    public class GetCompaniesResponse : IGetCompaniesResponse
    {
        public GetCompaniesResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetCompaniesResponse(IList<Company> companies,long databaseExecutionTime)
        {
            DatabaseExecutionTime = databaseExecutionTime;
            if (companies == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                Companies = companies;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public IList<Company> Companies { get; }

        public ProvideEnumResult ProvideResult { get; }
        public long DatabaseExecutionTime { get; }
    }
}
