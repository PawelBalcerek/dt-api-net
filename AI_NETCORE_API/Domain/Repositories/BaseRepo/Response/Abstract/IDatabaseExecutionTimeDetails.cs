using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories.BaseRepo.Response.Abstract
{
    public interface IDatabaseExecutionTimeDetails
    {
        long DatabaseExecutionTime { get; }
    }
}
