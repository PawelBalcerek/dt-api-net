using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Updaters.BuyOffers.Request.Abstract
{
    public interface IWithdrawBuyOfferByIdRequest
    {
        int BuyOfferId { get; }
    }
}
