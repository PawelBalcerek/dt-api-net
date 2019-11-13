using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.TransactionRepo.Abstract;
using Data.Models;
using Domain.DTOToBOConverting;
using System.Linq;
using System.Diagnostics;
using Domain.Repositories.BaseRepo.Response;
using Microsoft.EntityFrameworkCore;

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

        public RepositoryResponse<IEnumerable<BusinessObject.Transaction>> GetTransactionsByUserId(int id)
        {
            using (var databaseContext = new RepositoryContext())
            {
                var timer = Stopwatch.StartNew();
                var transactions = FindByCondition(p => p.SellOffer.Resource.UserId == id).Include(p => p.SellOffer).Include(p => p.SellOffer.Resource).Include(p => p.SellOffer.Resource.Comp).Select(p => _converter.ConvertTransaction(p));
                timer.Stop();
                var time = timer.ElapsedMilliseconds;
                return new RepositoryResponse<IEnumerable<BusinessObject.Transaction>>(transactions, time);
            }
        }
    }
}
