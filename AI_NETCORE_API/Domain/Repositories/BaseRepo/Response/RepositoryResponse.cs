using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories.BaseRepo.Response
{
    public class RepositoryResponse<T>
    {
        public RepositoryResponse(T o, long databaseTime)
        {
            Object = o;
            DatabaseTime = databaseTime;
        }

        public T Object { get; }
        public long DatabaseTime { get; }
    }
}
