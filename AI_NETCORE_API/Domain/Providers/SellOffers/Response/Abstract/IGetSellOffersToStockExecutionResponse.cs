using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Providers.Common.Abstract;
using Domain.Repositories.BaseRepo.Response.Abstract;

namespace Domain.Providers.SellOffers.Response.Abstract
{
    public interface IGetSellOffersToStockExecutionResponse : IProvideResult, IDatabaseExecutionTimeDetails
    {
        IList<SellOffer> SellOffers { get; }
    }
}
