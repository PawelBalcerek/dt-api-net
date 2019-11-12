using Domain.Creators.SellOffer.Response.Abstract;
using Domain.Providers.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.SellOffer.Response.Concrete
{
    public class SellOfferCreateResponse : ISellOfferCreateResponse
    {
        public SellOfferCreateResponse(bool success, long databaseExecutionTime)
        {
            Success = success;
            DatabaseExecutionTime = databaseExecutionTime;
            ProvideResult = ProvideEnumResult.Success;
        }

        public SellOfferCreateResponse()
        {
            Success = false;
            ProvideResult = ProvideEnumResult.Exception;
        }

        public bool Success { get; }
        public long DatabaseExecutionTime { get; }
        public ProvideEnumResult ProvideResult { get; }
    }
}
