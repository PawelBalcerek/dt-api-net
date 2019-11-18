using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.TransactionProcessing.Responses.Const
{
    public enum StockExchangeResultEnum
    {
        Exception = -1,
        Success=0,
        GetConfigurationWindowSizeFail = 1,
        GetSellOffersFail = 2,
        GetBuyOffersFail = 3,
        ProcessingWindowIsNotValid = 4
    }
}
