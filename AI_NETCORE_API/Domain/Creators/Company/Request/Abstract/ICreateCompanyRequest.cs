using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.Company.Request.Abstract
{
    public interface ICreateCompanyRequest
    {
        int UserId { get; }
        string Name { get; }
        int ResourceAmount { get; }
    }
}
