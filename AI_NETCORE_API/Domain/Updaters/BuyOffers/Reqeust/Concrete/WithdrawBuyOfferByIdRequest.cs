using Domain.Updaters.BuyOffers.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Updaters.BuyOffers.Request.Concrete
{
    public class WithdrawBuyOfferByIdRequest : IWithdrawBuyOfferByIdRequest
    {
        public WithdrawBuyOfferByIdRequest(int buyOfferId)
        {
            BuyOfferId = buyOfferId;
        }

        public int BuyOfferId { get; }
    }
}
