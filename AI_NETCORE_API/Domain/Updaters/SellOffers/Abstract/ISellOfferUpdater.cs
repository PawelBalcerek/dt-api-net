using Domain.Providers.SellOffers.Request.Abstract;
using Domain.Providers.SellOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Updaters.SellOffers.Abstract
{
    public interface ISellOfferUpdater
    {
        IWithdrawSellOfferByIdResponse WithdrawSellOfferById(IWithdrawSellOfferByIdRequest withdrawSellOfferByIdRequest);
    }
}
