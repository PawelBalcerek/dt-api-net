using Domain.BusinessObject;
using Domain.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Response.Abstract
{
    public interface IGetSellOffersResponse : IProvideResult
    {
        IList<SellOffer> SellOffers { get; }
    }
}
