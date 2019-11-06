using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories.BaseRepo.Response
{
    public class OnlyDatabaseTimeRepositoryResponse
    {
        public OnlyDatabaseTimeRepositoryResponse(long databaseTime)
        {
            DatabaseTime = databaseTime;
        }

        public long DatabaseTime { get; }
    }
}
