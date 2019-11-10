using Domain.BusinessObject;
using Domain.Providers.Common.Enum;
using Domain.Providers.SellOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Response.Concrete
{
    public class GetSellOffersByUserIdResponse : IGetSellOffersByUserIdResponse
    {
        public GetSellOffersByUserIdResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetSellOffersByUserIdResponse(IList<SellOffer> sellOffer, long databaseExecutionTime)
        {
            if (sellOffer == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                SellOffer = sellOffer;
                DatabaseExecutionTime = databaseExecutionTime;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public IList<SellOffer> SellOffer { get; }
        public long DatabaseExecutionTime { get; }
        public ProvideEnumResult ProvideResult { get; }
    }
}
