using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BuyOfferRepo.Abstract;
using Domain.Repositories.SellOfferRepo.Abstract;
using Domain.Repositories.CompanyRepo.Abstract;
using Domain.Repositories.ResourceRepo.Abstract;
using Domain.Repositories.UserRepo.Abstract;
using Domain.Repositories.TransactionRepo.Abstract;


namespace Domain.Repositories.Wrapper.Abstract
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ICompanyRepository Company { get; }
        IResourceRepository Resource { get; }
        IBuyOfferRepository BuyOffer { get; }
        ISellOfferRepository SellOffer { get; }
        ITransactionRepository Transaction { get; }
        void Save();
    }
}
