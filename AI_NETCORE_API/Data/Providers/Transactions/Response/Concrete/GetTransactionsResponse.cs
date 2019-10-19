using Data.BuisnessObject;
using Data.Providers.Common.Enum;
using Data.Providers.Transactions.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Transactions.Response.Concrete
{
    public class GetTransactionsResponse : IGetTransactionsResponse
    {
        public GetTransactionsResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetTransactionsResponse(IList<Transaction> transactions)
        {
            if (transactions == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                Transactions = transactions;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public IList<Transaction> Transactions { get; }

        public ProvideEnumResult ProvideResult { get; }
    }
}
