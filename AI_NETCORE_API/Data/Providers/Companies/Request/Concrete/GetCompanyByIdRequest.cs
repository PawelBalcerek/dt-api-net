using Data.Providers.Companies.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Companies.Request.Concrete
{
    public class GetCompanyByIdRequest : IGetCompanyByIdRequest
    {
        public GetCompanyByIdRequest(int companyId)
        {
            CompanyId = companyId;
        }

        public int CompanyId { get; }
    }
}
