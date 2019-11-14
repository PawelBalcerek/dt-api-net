using Domain.Infrastructure.OffersToTransactionsCalculating.Request.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Response.Abstract;

namespace Domain.Infrastructure.OffersToTransactionsCalculating.Abstract
{
    public interface IStockExchanger
    {
        IStockExchangeResponse StockExchange(IStockExchangeRequest stockExchangeRequest);
    }
}
