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
    public class SellOfferRepository : RepositoryBase<SellOffer>, ISellOfferRepository
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
            resource.Amount -= amount;
            RepositoryContext.SaveChanges(true);
            timer.Stop();
            var time = timer.ElapsedMilliseconds;
            return new RepositoryResponse<bool>(true, timer.ElapsedMilliseconds); ;
        }

        public long WithdrawSellOffer(int sellOfferId)
        {
            var timer = Stopwatch.StartNew();
            SellOffer offer = RepositoryContext.SellOffers.Where(p => p.Id == sellOfferId).FirstOrDefault();
            Resource resource = RepositoryContext.Resources.FirstOrDefault(r => r.Id == offer.ResourceId);
            resource.Amount += offer.Amount;
            offer.IsValid = false;

            RepositoryContext.SaveChanges(true);
            timer.Stop();
            var time = timer.ElapsedMilliseconds;
            return time;
        }

        public long ClearAll()
        {
            var tim = Stopwatch.StartNew();

            RepositoryContext.Database.ExecuteSqlCommand("DELETE FROM sell_offers");
            RepositoryContext.SaveChanges();

            return tim.ElapsedMilliseconds;
        }
        
        public RepositoryResponse<IEnumerable<BusinessObject.SellOffer>> GetSellOfferToStockExecute(int quantity,int companyId)
        {
            Stopwatch timer = Stopwatch.StartNew();
            IList<SellOffer> sellOffers = RepositoryContext.SellOffers.Where(x=> x.IsValid && x.Amount>0).Include(p => p.Resource).Include(p => p.Resource.Comp).OrderBy(x => x.Price).Where(so => so.Resource.Comp.Id == companyId).Take(quantity).ToList();
            timer.Stop();
            long time = timer.ElapsedMilliseconds;
            return new RepositoryResponse<IEnumerable<BusinessObject.SellOffer>>(sellOffers.Select(x=> _converter.ConvertSellOffer(x)), time);

        }
    }
}
