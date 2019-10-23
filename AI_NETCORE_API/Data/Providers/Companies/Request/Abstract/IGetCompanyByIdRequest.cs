using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Companies.Request.Abstract
{
    public interface IGetCompanyByIdRequest 
    {
        int CompanyId { get; }
    }
}
