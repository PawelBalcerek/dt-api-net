using Domain.BusinessObject;
using Domain.Providers.Common.Enum;
using Domain.Providers.Transactions.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Transactions.Response.Concrete
{
    public class GetTransactionsByUserIdResponse : IGetTransactionsByUserIdResponse
    {
        public GetTransactionsByUserIdResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetTransactionsByUserIdResponse(IList<Transaction> transaction, long databaseExecutionTime)
        {
            if (transaction == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                Transactions = transaction;
                DatabaseExecutionTime = databaseExecutionTime;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public IList<Transaction> Transactions { get; }
        public long DatabaseExecutionTime { get; }
        public ProvideEnumResult ProvideResult { get; }
    }
}
