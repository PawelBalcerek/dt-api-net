using Domain.BusinessObject;
using Domain.Providers.BuyOffers.Response.Abstract;
using Domain.Providers.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.BuyOffers.Response.Concrete
{
    class GetBuyOffersByUserIdResponse : IGetBuyOffersByUserIdResponse
    {
        public GetBuyOffersByUserIdResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetBuyOffersByUserIdResponse(IList<BuyOffer> buyOffer, long databaseExecutionTime)
        {
            if (buyOffer == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                BuyOffer = buyOffer;
                DatabaseExecutionTime = databaseExecutionTime;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public IList<BuyOffer> BuyOffer { get; }
        public long DatabaseExecutionTime { get; }
        public ProvideEnumResult ProvideResult { get; }
    }
}
