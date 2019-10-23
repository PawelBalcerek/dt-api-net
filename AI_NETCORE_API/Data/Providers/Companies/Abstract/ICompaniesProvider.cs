using Domain.Providers.Companies.Request.Abstract;
using Domain.Providers.Companies.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Companies.Abstract
{
    public interface ICompaniesProvider
    {
        IGetCompanyByIdResponse GetCompanyById(IGetCompanyByIdRequest getCompanyByIdRequest);
        IGetCompaniesResponse GetCompanies();
    }
}
