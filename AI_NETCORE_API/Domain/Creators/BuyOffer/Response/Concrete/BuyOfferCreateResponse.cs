using Domain.Creators.BuyOffer.Response.Abstract;
using Domain.Providers.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.BuyOffer.Response.Concrete
{
    public class BuyOfferCreateResponse : IBuyOfferCreateResponse
    {
        public BuyOfferCreateResponse(bool success, long databaseExecutionTime)
        {
            Success = success;
            DatabaseExecutionTime = databaseExecutionTime;
            ProvideResult = ProvideEnumResult.Success;
        }

        public BuyOfferCreateResponse()
        {
            Success = false;
            ProvideResult = ProvideEnumResult.Exception;
        }

        public bool Success { get; }
        public long DatabaseExecutionTime { get; }
        public ProvideEnumResult ProvideResult { get; }
    }
}
