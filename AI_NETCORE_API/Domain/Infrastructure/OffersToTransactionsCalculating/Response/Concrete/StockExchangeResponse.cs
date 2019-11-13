using System;
using System.Collections.Generic;
using System.Text;
using Domain.Creators.SellOffer.Request.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Response.Abstract;

namespace Domain.Infrastructure.OffersToTransactionsCalculating.Response.Concrete
{
    public class StockExchangeResponse : IStockExchangeResponse
    {

        public IList<ISellOfferCreateRequest> SellOffersToCreateAfterCalculating { get; }
        //public IList<IBuyOfferCreateRequest> OffersToCreateAfterCalculating { get; }
        //public IList<ITransactionCreateRequest> TransactionsTpCreateAfterCalculating { get; }
        public IList<IResource>
    }
}
