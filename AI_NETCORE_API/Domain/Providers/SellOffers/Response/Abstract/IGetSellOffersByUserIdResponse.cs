using Domain.BusinessObject;
using Domain.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Response.Abstract
{
    public interface IGetSellOffersByUserIdResponse : IProvideResult
    {
        List<SellOffer> SellOffer { get; }
    }
}
