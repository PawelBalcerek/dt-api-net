using Domain.Providers.Common.Abstract;
using Domain.BusinessObject;
using Domain.Repositories.BaseRepo.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Transactions.Response.Abstract
{
    public interface IGetTransactionsByUserIdResponse : IProvideResult, IDatabaseExecutionTimeDetails
    {
        IList<Transaction> Transactions { get; }
    }
}
