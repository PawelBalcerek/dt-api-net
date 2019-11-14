using Domain.Providers.BuyOffers.Request.Abstract;
using Domain.Providers.BuyOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.BuyOffers.Abstract
{
    public interface IBuyOffersProvider
    {
        IGetBuyOfferByIdResponse GetBuyOfferById(IGetBuyOfferByIdRequest getBuyOfferByIdRequest);
        
        IGetBuyOffersResponse GetBuyOffers();

        IGetBuyOffersToStockExecutionResponse GetBuyOfferToStockExecution(int quantity);
        IGetBuyOffersByUserIdResponse GetBuyOffersByUserId(IGetBuyOffersByUserIdRequest getBuyOffersByUserIdRequest);
        IWithdrawBuyOfferByIdResponse WithdrawBuyOfferById(IWithdrawBuyOfferByIdRequest withdrawBuyOfferByIdRequest);
    }
}
