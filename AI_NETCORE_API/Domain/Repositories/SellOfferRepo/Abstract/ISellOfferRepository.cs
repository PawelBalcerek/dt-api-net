using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Concrete;
using Data.Models;
using Domain.Repositories.BaseRepo.Response;

namespace Domain.Repositories.SellOfferRepo.Abstract
{
    public interface ISellOfferRepository : IRepositoryBase<SellOffer>
    {
        RepositoryResponse<IEnumerable<BusinessObject.SellOffer>> GetSellOffersByUserId(int id);
        long CreateSellOffer(int resourceId, int amount, double price);
        long WithdrawSellOffer(int sellOfferId);

        long ClearAll();

    }
}
