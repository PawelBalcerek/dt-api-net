using System;
using System.Collections.Generic;
using System.Text;
using Domain.Creators.SellOffer.Request.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Response.Abstract;
using Domain.Infrastructure.TransactionProcessing.Responses.Const;

namespace Domain.Infrastructure.OffersToTransactionsCalculating.Response.Concrete
{
    public class StockExchangeResponse : IStockExchangeResponse
    {
        public StockExchangeResponse(StockExchangeResultEnum result, long databaseTime)
        {
            Result = result;
            DatabaseTime = databaseTime;
        }

        public StockExchangeResultEnum Result { get; }
        public long DatabaseTime { get; }
    }
}
