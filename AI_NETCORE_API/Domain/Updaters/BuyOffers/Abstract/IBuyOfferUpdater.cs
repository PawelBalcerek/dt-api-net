using Domain.Updaters.BuyOffers.Request.Abstract;
using Domain.Updaters.BuyOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Updaters.BuyOffers.Abstract
{
    public interface IBuyOfferUpdater
    {
        IWithdrawBuyOfferByIdResponse WithdrawBuyOfferById(IWithdrawBuyOfferByIdRequest withdrawBuyOfferByIdRequest);
    }
}
