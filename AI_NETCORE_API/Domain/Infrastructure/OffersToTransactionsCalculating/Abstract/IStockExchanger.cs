using System;
using System.Collections.Generic;
using System.Text;
using Domain.Infrastructure.OffersToTransactionsCalculating.Response.Abstract;

namespace Domain.Infrastructure.OffersToTransactionsCalculating.Abstract
{
    public interface IStockExchanger
    {
        IStockExchangeResponse StockExchange();
    }
}
