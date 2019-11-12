using Domain.Providers.Common.Abstract;
using Domain.Repositories.BaseRepo.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.SellOffer.Response.Abstract
{
    public interface ISellOfferCreateResponse : IProvideResult, IDatabaseExecutionTimeDetails
    {
        bool Success { get; }
    }
}
