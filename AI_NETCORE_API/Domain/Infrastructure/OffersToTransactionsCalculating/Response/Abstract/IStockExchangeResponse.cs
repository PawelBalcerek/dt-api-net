using System;
using System.Collections.Generic;
using System.Text;
using Domain.Infrastructure.TransactionProcessing.Responses.Const;

namespace Domain.Infrastructure.OffersToTransactionsCalculating.Response.Abstract
{
    public interface IStockExchangeResponse
    {
        StockExchangeResultEnum Result { get; }
        long DatabaseTime { get; }
    }
}
