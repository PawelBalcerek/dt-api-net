using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Concrete;
using Data.Models;
using Domain.Creators.Transaction.Request.Abstract;
using Domain.Repositories.BaseRepo.Response;

namespace Domain.Repositories.TransactionRepo.Abstract
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        RepositoryResponse<IEnumerable<BusinessObject.Transaction>> GetTransactionsByUserId(int id);

        long SaveTransactionsAfterProcessing(IList<BusinessObject.SellOffer> sellOffersToSave,
            IList<BusinessObject.BuyOffer> buyOffersToSave, IList<ICreateTransactionRequest> transactionsToSave);
    }
}
