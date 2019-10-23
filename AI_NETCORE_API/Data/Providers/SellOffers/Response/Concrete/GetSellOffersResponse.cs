using Domain.BusinessObject;
using Domain.Providers.Common.Enum;
using Domain.Providers.SellOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Response.Concrete
{
    public class GetSellOffersResponse : IGetSellOffersResponse
    {
        public GetSellOffersResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public GetSellOffersResponse(IList<SellOffer> sellOffers)
        {
            if (sellOffers == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                SellOffers = sellOffers;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public ProvideEnumResult ProvideResult { get; }

        public IList<SellOffer> SellOffers { get; }
    }
}
