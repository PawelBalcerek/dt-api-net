using System;
using System.Collections.Generic;
using System.Text;
using Domain.Creators.Company.Request.Abstract;
using Domain.Creators.Company.Response.Abstract;

namespace Domain.Creators.Company.Abstract
{
    public interface ICompanyCreator
    {
        ICreateCompanyResponse CreateCompany(ICreateCompanyRequest createCompanyRequest);
    }
}
