using Data.BuisnessObject;
using Data.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Transactions.Response.Abstract
{
    public interface IGetTransactionsResponse : IProvideResult
    {
        IList<Transaction> Transactions { get; }
    }
}
