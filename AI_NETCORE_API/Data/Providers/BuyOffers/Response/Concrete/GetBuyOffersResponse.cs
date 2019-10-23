using Domain.BusinessObject;
using Domain.Providers.BuyOffers.Response.Abstract;
using Domain.Providers.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.BuyOffers.Response.Concrete
{
    public class GetBuyOffersResponse : IGetBuyOffersResponse
    {
        public GetBuyOffersResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetBuyOffersResponse(IList<BuyOffer> buyOffers)
        {
            if (buyOffers != null)
            {
                ProvideResult = ProvideEnumResult.Success;
                BuyOffers = buyOffers;
            }
            else
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
        }

        public IList<BuyOffer> BuyOffers { get; }

        public ProvideEnumResult ProvideResult { get; }
    }
}
