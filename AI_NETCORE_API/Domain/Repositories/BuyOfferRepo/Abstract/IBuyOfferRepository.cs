using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Concrete;
using Data.Models;
using Domain.Repositories.BaseRepo.Response;

namespace Domain.Repositories.BuyOfferRepo.Abstract
{
    public interface IBuyOfferRepository : IRepositoryBase<BuyOffer>
    {
        RepositoryResponse<IEnumerable<BusinessObject.BuyOffer>> GetBuyOffersByUserId(int id);
    }
}
