using Domain.BusinessObject;
using Domain.Providers.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.BuyOffers.Response.Abstract
{
    public interface IGetBuyOffersResponse :IProvideResult
    {
        IList<BuyOffer> BuyOffers { get; }
    }
}
