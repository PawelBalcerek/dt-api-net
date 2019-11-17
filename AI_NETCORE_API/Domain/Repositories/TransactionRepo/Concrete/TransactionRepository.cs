using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.TransactionRepo.Abstract;
using Data.Models;
using Domain.DTOToBOConverting;
using System.Linq;
using System.Diagnostics;
using Domain.Creators.Transaction.Request.Abstract;
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

        public long SaveTransactionsAfterProcessing(IList<BusinessObject.SellOffer> sellOffersToSave,
            IList<BusinessObject.BuyOffer> buyOffersToSave, IList<ICreateTransactionRequest> transactionsToSave)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (ICreateTransactionRequest transactionRequest in transactionsToSave)
            {

                Transaction transaction = new Transaction
                {
                    Amount = transactionRequest.Amount,
                    Date = transactionRequest.CreationDate,
                    Price = transactionRequest.Price,
                    BuyOfferId = transactionRequest.BuyOfferId,
                    SellOfferId = transactionRequest.SellOfferId
                };
                RepositoryContext.Transactions.Add(transaction);

                BusinessObject.SellOffer sellOffer =
                    sellOffersToSave.FirstOrDefault(x => x.Id == transactionRequest.SellOfferId);

                if(sellOffer == null) throw new InvalidOperationException("Cannot create transaction without sellOffer");

                RepositoryContext.SellOffers.Update(new SellOffer
                {
                    Price = sellOffer.Price,
                    Amount = sellOffer.Amount,
                    Id = sellOffer.Id,
                    Date = sellOffer.Date,
                    IsValid = sellOffer.IsValid,
                    ResourceId = sellOffer.ResourceId,
                    StartAmount = sellOffer.StartAmount
                });

                Resource sellOfferResource = RepositoryContext.Resources.FirstOrDefault(x => x.Id == sellOffer.ResourceId);

                if(sellOfferResource == null) throw new InvalidOperationException("Resources from sellOffer not found");

                sellOfferResource.Amount -= transaction.Amount;
                RepositoryContext.Resources.Update(sellOfferResource);




                BusinessObject.BuyOffer buyOffer =
                    buyOffersToSave.FirstOrDefault(x => x.Id == transactionRequest.BuyOfferId);

                if (buyOffer == null) throw new InvalidOperationException("Cannot create transaction without buyOffer");

                RepositoryContext.BuyOffers.Update(new BuyOffer
                {
                    MaxPrice = buyOffer.MaxPrice,
                    Amount = buyOffer.Amount,
                    Id = buyOffer.Id,
                    Date = buyOffer.Date,
                    IsValid = buyOffer.IsValid,
                    ResourceId = buyOffer.ResourceId,
                    StartAmount = buyOffer.StartAmount
                });

                Resource buyOfferResource = RepositoryContext.Resources.FirstOrDefault(x => x.Id == buyOffer.ResourceId);

                if (buyOfferResource == null) throw new InvalidOperationException("Resources from buyOffer not found");

                buyOfferResource.Amount -= transaction.Amount;
                RepositoryContext.Resources.Update(buyOfferResource);

            }
            RepositoryContext.SaveChanges();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
