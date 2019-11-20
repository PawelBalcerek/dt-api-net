using Domain.Providers.SellOffers.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Request.Concrete
{
    public class WithdrawSellOfferByIdRequest : IWithdrawSellOfferByIdRequest
    {
        public WithdrawSellOfferByIdRequest(int sellOfferId)
        {
            SellOfferId = sellOfferId;
        }

        public int SellOfferId { get; }
    }
}
