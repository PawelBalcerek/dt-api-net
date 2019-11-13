using System;
using System.Collections.Generic;
using System.Text;
using Domain.Providers.SellOffers.Request.Abstract;

namespace Domain.Providers.SellOffers.Request.Concrete
{
    public class GetSellOffersToStockExecutionRequest : IGetSellOffersToStockExecutionRequest
    {
        public GetSellOffersToStockExecutionRequest(int quantity)
        {
            Quantity = quantity;
        }

        public int Quantity { get; }
    }
}
