using System;
using System.Collections.Generic;
using System.Text;
using Domain.Infrastructure.OffersToTransactionsCalculating.Request.Abstract;

namespace Domain.Infrastructure.OffersToTransactionsCalculating.Request.Concrete
{
    public class StockExchangeRequest : IStockExchangeRequest
    {
        public StockExchangeRequest(int companyId)
        {
            CompanyId = companyId;
        }

        public int CompanyId { get; }
    }
}
