using Data.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Transactions.Response.Abstract
{
    public interface IGetTransactionByIdResponse : IProvideResult
    {
        dynamic Transaction { get; }
    }
}
