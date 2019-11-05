using Domain.Providers.BuyOffers.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.BuyOffers.Request.Concrete
{
    public class GetBuyOfferByIdRequest : IGetBuyOfferByIdRequest
    {
        public GetBuyOfferByIdRequest(int buyOfferId)
        {
            BuyOfferId = buyOfferId;
        }

        public int BuyOfferId { get; }
    }
}
