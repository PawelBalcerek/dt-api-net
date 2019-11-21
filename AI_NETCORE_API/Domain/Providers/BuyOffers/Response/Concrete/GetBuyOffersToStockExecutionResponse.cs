using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Providers.BuyOffers.Response.Abstract;
using Domain.Providers.Common.Enum;

namespace Domain.Providers.BuyOffers.Response.Concrete
{
    public class GetBuyOffersToStockExecutionResponse : IGetBuyOffersToStockExecutionResponse
    {
        public GetBuyOffersToStockExecutionResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetBuyOffersToStockExecutionResponse(IList<BuyOffer> buyOffers, long databaseExecutionTime)
        {
            if (buyOffers == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                BuyOffers = buyOffers;
                DatabaseExecutionTime = databaseExecutionTime;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public ProvideEnumResult ProvideResult { get; }
        public long DatabaseExecutionTime { get; }
        public IList<BuyOffer> BuyOffers { get; }
    }
}
