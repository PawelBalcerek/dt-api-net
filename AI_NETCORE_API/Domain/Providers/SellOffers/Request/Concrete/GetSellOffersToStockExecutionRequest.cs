using System;
using System.Collections.Generic;
using System.Text;
using Domain.Providers.SellOffers.Request.Abstract;

namespace Domain.Providers.SellOffers.Request.Concrete
{
    public class GetSellOffersToStockExecutionRequest : IGetSellOffersToStockExecutionRequest
    {
        public GetSellOffersToStockExecutionRequest(int quantity, int companyId)
        {
            Quantity = quantity;
            CompanyId = companyId;
        }

        public int Quantity { get; }
        public int CompanyId { get; }
    }
}
