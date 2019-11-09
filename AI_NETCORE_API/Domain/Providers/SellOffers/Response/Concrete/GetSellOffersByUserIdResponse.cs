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

        public GetSellOffersByUserIdResponse(List<SellOffer> sellOffer)
        {
            if (sellOffer == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                SellOffer = sellOffer;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public List<SellOffer> SellOffer { get; }

        public ProvideEnumResult ProvideResult { get; }
    }
}
