using Domain.Providers.SellOffers.Request.Abstract;
using Domain.Providers.SellOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Abstract
{
    public interface ISellOfferProvider
    {
        IGetSellOffersByUserIdResponse GetSellOffersByUserId(IGetSellOffersByUserIdRequest getSellOffersByUserIdRequest);
        IWithdrawSellOfferByIdResponse WithdrawSellOfferById(IWithdrawSellOfferByIdRequest withdrawSellOfferByIdRequest);

        IGetSellOffersToStockExecutionResponse GetSellOfferToStockExecute(int quantity);
    }
}
