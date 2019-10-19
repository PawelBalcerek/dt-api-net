using Data.BuisnessObject;
using Data.Providers.Common.Enum;
using Data.Providers.Companies.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Companies.Response.Concrete
{
    public class GetCompanyByIdResponse : IGetCompanyByIdResponse
    {
        public GetCompanyByIdResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetCompanyByIdResponse(Company company)
        {
            if (company == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                Company = company;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public Company Company { get; }

        public ProvideEnumResult ProvideResult { get; }
    }
}
