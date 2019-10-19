using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Companies.Request.Abstract
{
    public interface IGetCompanyByIdRequest 
    {
        int CompanyId { get; }
    }
}
