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
        RepositoryResponse<bool> CreateSellOffer(int resourceId, int amount, double price, int userId);
        long WithdrawSellOffer(int sellOfferId);
        RepositoryResponse<IEnumerable<BusinessObject.SellOffer>> GetSellOfferToStockExecute(int quantity,int companyId);
    }
}
