﻿using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Concrete;
using Data.Models;
namespace Domain.Repositories.SellOfferRepo.Abstract
{
    public interface ISellOfferRepository : IRepositoryBase<SellOffer>
    {
        BusinessObject.SellOffer GetSellOfferById(int id);
        IEnumerable<BusinessObject.SellOffer> GetAllSellOffers();
        List<BusinessObject.SellOffer> GetSellOffersByUserId(int id);
    }
}
