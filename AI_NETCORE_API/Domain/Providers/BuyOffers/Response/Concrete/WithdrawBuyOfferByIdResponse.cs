using Domain.Providers.BuyOffers.Response.Abstract;
using Domain.Providers.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.BuyOffers.Response.Concrete
{
    public class WithdrawBuyOfferByIdResponse : IWithdrawBuyOfferByIdResponse
    {
        public WithdrawBuyOfferByIdResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }

        public WithdrawBuyOfferByIdResponse(long databaseExecutionTime)
        {
            DatabaseExecutionTime = databaseExecutionTime;
            ProvideResult = ProvideEnumResult.Success;
        }

        public long DatabaseExecutionTime { get; }
        public ProvideEnumResult ProvideResult { get; }
    }
}
