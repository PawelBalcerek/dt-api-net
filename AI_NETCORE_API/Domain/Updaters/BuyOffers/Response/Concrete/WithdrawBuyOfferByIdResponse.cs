using Domain.Providers.Common.Enum;
using Domain.Updaters.BuyOffers.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Updaters.BuyOffers.Response.Concrete
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
