using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.SellOfferRepo.Abstract;
using Data.Models;

namespace Domain.Repositories.SellOfferRepo.Concrete
{
    public class SellOfferRepository: RepositoryBase<SellOffer>, ISellOfferRepository
    {
        public SellOfferRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
