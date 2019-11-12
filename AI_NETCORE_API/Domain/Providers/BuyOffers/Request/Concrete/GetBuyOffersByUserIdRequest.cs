using Domain.Providers.BuyOffers.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.BuyOffers.Request.Concrete
{
    public class GetBuyOffersByUserIdRequest : IGetBuyOffersByUserIdRequest
    {
        public GetBuyOffersByUserIdRequest(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}
