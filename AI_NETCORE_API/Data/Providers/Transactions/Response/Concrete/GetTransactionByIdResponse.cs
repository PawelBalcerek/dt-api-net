using Data.Providers.Common.Enum;
using Data.Providers.Transactions.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Transactions.Response.Concrete
{
    public class GetTransactionByIdResponse : IGetTransactionByIdResponse
    {
        public GetTransactionByIdResponse()
        {
            ProvideResult = ProvideEnumResult.Exception;
        }
        public GetTransactionByIdResponse(dynamic transaction)
        {
            if (transaction == null)
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
            else
            {
                Transaction = transaction;
                ProvideResult = ProvideEnumResult.Success;
            }
        }

        public dynamic Transaction { get; }

        public ProvideEnumResult ProvideResult { get; }
    }
}
