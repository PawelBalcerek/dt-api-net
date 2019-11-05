using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.Wrapper.Abstract;
using Domain.Repositories.BuyOfferRepo.Abstract;
using Domain.Repositories.SellOfferRepo.Abstract;
using Domain.Repositories.CompanyRepo.Abstract;
using Domain.Repositories.ResourceRepo.Abstract;
using Domain.Repositories.UserRepo.Abstract;
using Domain.Repositories.TransactionRepo.Abstract;
using Domain.Repositories.BuyOfferRepo.Concrete;
using Domain.Repositories.SellOfferRepo.Concrete;
using Domain.Repositories.CompanyRepo.Concrete;
using Domain.Repositories.ResourceRepo.Concrete;
using Domain.Repositories.UserRepo.Concrete;
using Domain.Repositories.TransactionRepo.Concrete;
using Data.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
/*
namespace Domain.Repositories.Wrapper.Concrete
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IUserRepository _user;
        private ICompanyRepository _company;
        private IResourceRepository _resource;
        private IBuyOfferRepository _buyOffer;
        private ISellOfferRepository _sellOffer;
        private ITransactionRepository _transaction;

        public IUserRepository User
        {
            get
            {
                if(_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }

                return _user;
            }
        }

        public ICompanyRepository Company
        {
            get
            {
                if (_company == null)
                {
                    _company = new CompanyRepository(_repoContext);
                }

                return _company;
            }
        }

        public IResourceRepository Resource
        {
            get
            {
                if (_resource == null)
                {
                    _resource = new ResourceRepository(_repoContext);
                }

                return _resource;
            }
        }

        public IBuyOfferRepository BuyOffer
        {
            get
            {
                if (_buyOffer == null)
                {
                    _buyOffer = new BuyOfferRepository(_repoContext);
                }

                return _buyOffer;
            }
        }

        public ISellOfferRepository SellOffer
        {
            get
            {
                if (_sellOffer == null)
                {
                    _sellOffer = new SellOfferRepository(_repoContext);
                }

                return _sellOffer;
            }
        }

        public ITransactionRepository Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _transaction = new TransactionRepository(_repoContext);
                }

                return _transaction;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }

    }


}
*/