using Data.BuisnessObject;
using Data.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.BuyOffers.Response.Abstract
{
    public interface IGetBuyOffersResponse :IProvideResult
    {
        IList<BuyOffer> BuyOffers { get; }
    }
}
