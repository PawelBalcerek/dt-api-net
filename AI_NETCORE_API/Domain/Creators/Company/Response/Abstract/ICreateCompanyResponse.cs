using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Response.Abstract;

namespace Domain.Creators.Company.Response.Abstract
{
    public interface ICreateCompanyResponse : IDatabaseExecutionTimeDetails
    {
        bool Success { get; }
    }
}
