using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.BuyOfferRepo.Abstract;
using Data.Models;
namespace Domain.Repositories.BuyOfferRepo.Concrete
{
    public class BuyOfferRepository: RepositoryBase<BuyOffer>, IBuyOfferRepository
    {
        public BuyOfferRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
