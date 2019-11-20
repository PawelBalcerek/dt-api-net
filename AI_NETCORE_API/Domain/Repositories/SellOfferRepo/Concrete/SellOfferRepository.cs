using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.SellOfferRepo.Abstract;
using Data.Models;
using Domain.DTOToBOConverting;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories.BaseRepo.Response;
using System.Diagnostics;

namespace Domain.Repositories.SellOfferRepo.Concrete
{
    public class SellOfferRepository: RepositoryBase<SellOffer>, ISellOfferRepository
    {
        private readonly IDTOToBOConverter _converter;
        public SellOfferRepository(RepositoryContext repositoryContext, IDTOToBOConverter converter)
            : base(repositoryContext)
        {
            _converter = converter;
        }

        public RepositoryResponse<IEnumerable<BusinessObject.SellOffer>> GetSellOffersByUserId(int id)
        {
            using (var databaseContext = new RepositoryContext())
            {
                var timer = Stopwatch.StartNew();
                var sellOffers = FindByCondition(p => p.Resource.UserId == id).Include(p => p.Resource).Include(p => p.Resource.Comp).Select(p => _converter.ConvertSellOffer(p));
                timer.Stop();
                var time = timer.ElapsedMilliseconds;
                return new RepositoryResponse<IEnumerable<BusinessObject.SellOffer>>(sellOffers, time);
            }
            
        }

        public RepositoryResponse<bool> CreateSellOffer(int resourceId, int amount, double price, int userId)
        {
            var timer = Stopwatch.StartNew();

            var resource = RepositoryContext.Resources.Where(p => p.Id == resourceId).FirstOrDefault();


            // User doesn't have this resource
            if (resource == null)
            {
                timer.Stop();
                return new RepositoryResponse<bool>(false, timer.ElapsedMilliseconds);
            }
            // This resoure doesn't belong to current user
            else if(resource.UserId != userId)
            {
                timer.Stop();
                return new RepositoryResponse<bool>(false, timer.ElapsedMilliseconds);
            }
            // User doesn't have enough amount
            else if (resource.Amount < amount)
            {
                timer.Stop();
                return new RepositoryResponse<bool>(false, timer.ElapsedMilliseconds);
            }

            RepositoryContext.SellOffers.Add(new SellOffer
            {
                Amount = amount,
                StartAmount = amount,
                IsValid = true,
                Price = price,
                ResourceId = resourceId,
                Date = DateTime.Now
            });
            RepositoryContext.SaveChanges(true);
            timer.Stop();
            var time = timer.ElapsedMilliseconds;
            return new RepositoryResponse<bool>(true, timer.ElapsedMilliseconds); ;
        }

        public long WithdrawSellOffer(int sellOfferId)
        {
            var timer = Stopwatch.StartNew();
            using (var dbContext = new RepositoryContext())
            {
                SellOffer offer = dbContext.SellOffers.Where(p => p.Id == sellOfferId).First();
                offer.IsValid = false;

            }
            RepositoryContext.SaveChanges(true);
            timer.Stop();
            var time = timer.ElapsedMilliseconds;
            return time;
        }
    }
}
