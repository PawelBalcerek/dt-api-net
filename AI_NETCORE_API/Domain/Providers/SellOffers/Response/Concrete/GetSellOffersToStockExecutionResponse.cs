using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Providers.Common.Enum;
using Domain.Providers.SellOffers.Response.Abstract;

namespace Domain.Providers.SellOffers.Response.Concrete
{
    public class GetSellOffersToStockExecutionResponse : IGetSellOffersToStockExecutionResponse
    {
        public GetSellOffersToStockExecutionResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetSellOffersToStockExecutionResponse(IList<SellOffer> sellOffers, long databaseExecutionTime)
        {
            if (sellOffers == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                SellOffers = sellOffers;
                DatabaseExecutionTime = databaseExecutionTime;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public ProvideEnumResult ProvideResult { get; }
        public long DatabaseExecutionTime { get; }
        public IList<SellOffer> SellOffers { get; }
    }
}
