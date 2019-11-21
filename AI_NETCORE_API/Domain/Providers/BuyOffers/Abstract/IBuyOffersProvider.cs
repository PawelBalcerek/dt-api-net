using Domain.Providers.BuyOffers.Request.Abstract;
using Domain.Providers.BuyOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.BuyOffers.Abstract
{
    public interface IBuyOffersProvider
    {
        IGetBuyOffersToStockExecutionResponse GetBuyOfferToStockExecution(IGetBuyOffersToStockExecutionRequest getBuyOffersToStockExecutionRequest);
        IGetBuyOffersByUserIdResponse GetBuyOffersByUserId(IGetBuyOffersByUserIdRequest getBuyOffersByUserIdRequest);
    }
}
