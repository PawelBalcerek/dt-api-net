using Data.BuisnessObject;
using Data.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.BuyOffers.Response.Abstract
{
    public interface IGetBuyOfferByIdResponse : IProvideResult
    {
        BuyOffer BuyOffer { get; }
    }
}
