using Domain.Providers.Common.Enum;
using Domain.Providers.SellOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Response.Concrete
{
    public class WithdrawSellOfferByIdResponse : IWithdrawSellOfferByIdResponse
    {
        public WithdrawSellOfferByIdResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public WithdrawSellOfferByIdResponse(long databaseExecutionTime)
        {
            DatabaseExecutionTime = databaseExecutionTime;
            ProvideResult = ProvideEnumResult.Success;
        }
        
        public long DatabaseExecutionTime { get; }
        public ProvideEnumResult ProvideResult { get; }
    }
}
