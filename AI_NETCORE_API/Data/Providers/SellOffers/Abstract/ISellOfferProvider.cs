using Data.Providers.SellOffers.Request.Abstract;
using Data.Providers.SellOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.SellOffers.Abstract
{
    public interface ISellOfferProvider
    {
        IGetSellOfferByIdResponse GetSellOfferById(IGetSellOfferByIdRequest getSellOfferByIdRequest);
        IGetSellOffersResponse GetSellOffers();
    }
}
