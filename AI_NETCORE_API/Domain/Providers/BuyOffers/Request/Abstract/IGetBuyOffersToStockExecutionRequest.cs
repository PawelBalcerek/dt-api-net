using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.BuyOffers.Request.Abstract
{
    public interface IGetBuyOffersToStockExecutionRequest
    {
        int Quantity { get; }
        int CompanyId { get; }
    }
}
