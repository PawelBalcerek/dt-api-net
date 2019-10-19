using Data.Providers.BuyOffers.Request.Abstract;
using Data.Providers.BuyOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.BuyOffers.Abstract
{
    public interface IBuyOffersProvider
    {
        IGetBuyOfferByIdResponse GetBuyOfferById(IGetBuyOfferByIdRequest getBuyOfferByIdRequest);
        IGetBuyOffersResponse GetBuyOffers();
    }
}
