using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.TransactionRepo.Abstract;
using Data.Models;
using Domain.DTOToBOConverting;
using System.Linq;
namespace Domain.Repositories.TransactionRepo.Concrete
{
    public class TransactionRepository: RepositoryBase<Transaction>, ITransactionRepository
    {
        private readonly IDTOToBOConverter _converter;
        public TransactionRepository(RepositoryContext repositoryContext, IDTOToBOConverter converter)
            : base(repositoryContext)
        {
            _converter = converter;
        }

        public BusinessObject.Transaction GetTransactionById(int id)
        {
            Transaction transaction = FindByCondition(transExpr => transExpr.Id == id).FirstOrDefault();
            return _converter.ConvertTransaction(transaction);
        }

        public IEnumerable<BusinessObject.Transaction> GetAllTransactions()
        {
            return FindAll().Select(t => _converter.ConvertTransaction(t));
        }
    }
}
