using Domain.BuisnessObject;
using Domain.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.BuyOffers.Response.Abstract
{
    public interface IGetBuyOfferByIdResponse : IProvideResult
    {
        BuyOffer BuyOffer { get; }
    }
}
