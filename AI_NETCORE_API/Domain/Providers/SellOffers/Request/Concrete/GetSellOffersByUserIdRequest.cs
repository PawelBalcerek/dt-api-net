using Domain.Providers.SellOffers.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Request.Concrete
{
    public class GetSellOffersByUserIdRequest : IGetSellOffersByUserIdRequest
    {
        public GetSellOffersByUserIdRequest(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }   
}
