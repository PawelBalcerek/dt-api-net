using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Concrete;
using Data.Models;
using Domain.Repositories.BaseRepo.Response;

namespace Domain.Repositories.TransactionRepo.Abstract
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        BusinessObject.Transaction GetTransactionById(int id);
        IEnumerable<BusinessObject.Transaction> GetAllTransactions();
        RepositoryResponse<IEnumerable<BusinessObject.Transaction>> GetTransactionsByUserId(int id);
    }
}
