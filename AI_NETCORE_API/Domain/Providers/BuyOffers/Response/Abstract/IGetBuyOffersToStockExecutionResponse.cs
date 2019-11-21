using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Providers.Common.Abstract;
using Domain.Repositories.BaseRepo.Response.Abstract;

namespace Domain.Providers.BuyOffers.Response.Abstract
{
    public interface IGetBuyOffersToStockExecutionResponse : IProvideResult, IDatabaseExecutionTimeDetails
    {
        IList<BuyOffer> BuyOffers { get; }
    }
}
