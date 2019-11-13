using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Request.Abstract
{
    public interface IGetSellOffersToStockExecutionRequest
    {
        int Quantity { get; }
    }
}
