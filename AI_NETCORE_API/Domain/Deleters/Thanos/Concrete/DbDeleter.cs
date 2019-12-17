using Domain.Deleters.Thanos.Abstract;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Repositories.BuyOfferRepo.Abstract;
using Domain.Repositories.CompanyRepo.Abstract;
using Domain.Repositories.ResourceRepo.Abstract;
using Domain.Repositories.SellOfferRepo.Abstract;
using Domain.Repositories.TransactionRepo.Abstract;
using Domain.Repositories.UserRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Domain.Deleters.Thanos.Concrete
{
    public class DbDeleter : IDbDeleter
    {
        ILogger _logger;
        IBuyOfferRepository _buyOfferRepository;
        ICompanyRepository _companyRepository;
        IResourceRepository _resourceRepository;
        ISellOfferRepository _sellOfferRepositroy;
        ITransactionRepository _transactionRepository;
        IUserRepository _userRepository;

        public DbDeleter(ILogger logger,
            IBuyOfferRepository buyOfferRepository,
            ICompanyRepository companyRepository,
            IResourceRepository resourceRepository,
            ISellOfferRepository sellOfferRepositroy,
            ITransactionRepository transactionRepository,
            IUserRepository userRepository)
        {
            _logger = logger;
            _buyOfferRepository = buyOfferRepository;
            _companyRepository = companyRepository;
            _resourceRepository = resourceRepository;
            _sellOfferRepositroy = sellOfferRepositroy;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }

        public long Purge()
        {
            long tim = 0;

            tim += _transactionRepository.ClearAll();
            tim += _buyOfferRepository.ClearAll();
            tim += _sellOfferRepositroy.ClearAll();
            tim += _resourceRepository.ClearAll();
            tim += _companyRepository.ClearAll();
            tim += _userRepository.ClearAll();

            return tim;
        }

        public long Clear()
        {

            long tim = 0;

            tim += _transactionRepository.ClearAll();
            tim += _buyOfferRepository.ClearAll();
            tim += _sellOfferRepositroy.ClearAll();
            tim += _resourceRepository.ClearAll();
            tim += _companyRepository.ClearAll();
            tim += _userRepository.ClearCash();

            return tim;
        }
    }
}
