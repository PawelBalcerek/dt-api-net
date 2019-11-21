using Domain.Providers.Common.Abstract;
using Domain.Repositories.BaseRepo.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Updaters.BuyOffers.Response.Abstract
{
    public interface IWithdrawBuyOfferByIdResponse : IProvideResult, IDatabaseExecutionTimeDetails
    {
    }
}
