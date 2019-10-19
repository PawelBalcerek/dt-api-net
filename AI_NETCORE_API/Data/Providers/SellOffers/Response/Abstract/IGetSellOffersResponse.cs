using Data.BuisnessObject;
using Data.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.SellOffers.Response.Abstract
{
    public interface IGetSellOffersResponse : IProvideResult
    {
        IList<SellOffer> SellOffers { get; }
    }
}
