using Domain.Providers.SellOffers.Request.Abstract;
using Domain.Providers.SellOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Abstract
{
    public interface ISellOfferProvider
    {
        IGetSellOfferByIdResponse GetSellOfferById(IGetSellOfferByIdRequest getSellOfferByIdRequest);
        IGetSellOffersResponse GetSellOffers();
        List<IGetSellOffersByUserIdResponse> GetSellOffersByUserId(IGetSellOffersByUserIdRequest getSellOffersByUserIdRequest);
    }
}
