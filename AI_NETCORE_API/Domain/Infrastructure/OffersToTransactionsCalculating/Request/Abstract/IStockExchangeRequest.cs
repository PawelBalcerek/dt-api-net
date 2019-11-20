using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.OffersToTransactionsCalculating.Request.Abstract
{
    public interface IStockExchangeRequest
    {
        int CompanyId { get; }
    }
}
