using Domain.BusinessObject;
using Domain.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Companies.Response.Abstract
{
    public interface IGetCompanyByIdResponse : IProvideResult
    {
        Company Company { get; }
    }
}
