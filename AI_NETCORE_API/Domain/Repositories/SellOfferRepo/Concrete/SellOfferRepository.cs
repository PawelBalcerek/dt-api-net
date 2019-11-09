﻿using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.SellOfferRepo.Abstract;
using Data.Models;
using Domain.DTOToBOConverting;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public BusinessObject.SellOffer GetSellOfferById(int id)
        {
            SellOffer sellOffer = FindByCondition(sellOfferExpr => sellOfferExpr.Id == id).FirstOrDefault();
            return _converter.ConvertSellOffer(sellOffer);
        }

        public IEnumerable<BusinessObject.SellOffer> GetAllSellOffers()
        {
            return FindAll().Select(s => _converter.ConvertSellOffer(s));
        }

        public List<BusinessObject.SellOffer> GetSellOffersByUserId(int id)
        {
            using (var databaseContext = new RepositoryContext())
            {
                List<SellOffer> sellOffers = databaseContext.SellOffers.Where(p => p.Resource.UserId == id && p.IsValid == true).Include(p => p.Resource).ToList();
                List<BusinessObject.SellOffer> bussinessSellOffers = new List<BusinessObject.SellOffer>();
                foreach(var sellOffer in sellOffers)
                {
                    bussinessSellOffers.Add(_converter.ConvertSellOfferWithCompany(sellOffer, sellOffer.Resource.Comp));
                }
                return bussinessSellOffers;
            }
        }
    }
}
