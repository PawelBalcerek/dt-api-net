using System;
using System.Collections.Generic;
using System.Text;
using Domain.Providers.BuyOffers.Request.Abstract;

namespace Domain.Providers.BuyOffers.Request.Concrete
{
    public class GetBuyOffersToStockExecutionRequest : IGetBuyOffersToStockExecutionRequest
    {
        public GetBuyOffersToStockExecutionRequest(int quantity, int companyId)
        {
            Quantity = quantity;
            CompanyId = companyId;
        }

        public int Quantity { get; }
        public int CompanyId { get; }
    }
}
