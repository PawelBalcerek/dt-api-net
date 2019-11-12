using System;
using System.Collections.Generic;
using System.Text;
using Domain.Creators.Company.Response.Abstract;

namespace Domain.Creators.Company.Response.Concrete
{
    public class CreateCompanyResponse : ICreateCompanyResponse
    {
        public CreateCompanyResponse(long databaseExecutionTime)
        {
            DatabaseExecutionTime = databaseExecutionTime;
            Success = true;
        }

        public CreateCompanyResponse()
        {
            Success = false;
        }

        public long DatabaseExecutionTime { get; }
        public bool Success { get; }
    }
}
