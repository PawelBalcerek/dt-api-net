using Domain.Providers.Common.Abstract;
using Domain.Repositories.BaseRepo.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Response.Abstract
{
    public interface IWithdrawSellOfferByIdResponse : IProvideResult, IDatabaseExecutionTimeDetails
    {
    }
}
