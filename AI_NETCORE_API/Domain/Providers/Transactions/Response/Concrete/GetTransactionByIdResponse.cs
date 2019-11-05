using Domain.Providers.Common.Enum;
using Domain.Providers.Transactions.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Transactions.Response.Concrete
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
