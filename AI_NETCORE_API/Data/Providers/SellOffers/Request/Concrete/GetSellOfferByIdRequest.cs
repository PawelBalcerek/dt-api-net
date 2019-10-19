using Data.Providers.SellOffers.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.SellOffers.Request.Concrete
{
    public class GetSellOfferByIdRequest : IGetSellOfferByIdRequest
    {
        public GetSellOfferByIdRequest(int sellOfferId)
        {
            SellOfferId = sellOfferId;
        }

        public int SellOfferId { get; }
    }
}
