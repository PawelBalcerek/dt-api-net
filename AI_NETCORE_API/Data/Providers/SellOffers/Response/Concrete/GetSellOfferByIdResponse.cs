using Data.BuisnessObject;
using Data.Providers.Common.Enum;
using Data.Providers.SellOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.SellOffers.Response.Concrete
{
    public class GetSellOfferByIdResponse : IGetSellOfferByIdResponse
    {
        public GetSellOfferByIdResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetSellOfferByIdResponse(SellOffer sellOffer)
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

        public SellOffer SellOffer { get; }

        public ProvideEnumResult ProvideResult { get; }
    }
}
