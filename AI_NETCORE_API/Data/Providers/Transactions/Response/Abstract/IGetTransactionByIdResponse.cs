using Domain.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Transactions.Response.Abstract
{
    public interface IGetTransactionByIdResponse : IProvideResult
    {
        dynamic Transaction { get; }
    }
}
