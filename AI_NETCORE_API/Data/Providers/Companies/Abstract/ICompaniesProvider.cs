using Data.Providers.Companies.Request.Abstract;
using Data.Providers.Companies.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Companies.Abstract
{
    public interface ICompaniesProvider
    {
        IGetCompanyByIdResponse GetCompanyById(IGetCompanyByIdRequest getCompanyByIdRequest);
        IGetCompaniesResponse GetCompanies();
    }
}
