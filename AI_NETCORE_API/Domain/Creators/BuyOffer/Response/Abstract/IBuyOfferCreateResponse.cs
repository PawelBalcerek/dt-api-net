using Domain.Providers.Common.Abstract;
using Domain.Repositories.BaseRepo.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.BuyOffer.Response.Abstract
{
    public interface IBuyOfferCreateResponse : IProvideResult, IDatabaseExecutionTimeDetails
    {
        bool Success { get; }
    }
}
