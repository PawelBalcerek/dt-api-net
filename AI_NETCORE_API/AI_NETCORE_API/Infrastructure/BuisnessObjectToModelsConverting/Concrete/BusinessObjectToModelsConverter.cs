using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using Domain.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Concrete
{
    internal class BusinessObjectToModelsConverter : IBusinessObjectToModelsConverter
    {
        public BuyOfferModel ConvertBuyOffer(BuyOffer buyOffer)
        {
            return new BuyOfferModel
            {
                Id = buyOffer.Id,
                Amount = buyOffer.Amount,
                StartAmount = buyOffer.StartAmount,
                Date = buyOffer.Date,
                IsValid = buyOffer.IsValid,
                MaxPrice = buyOffer.MaxPrice,
                Company = new CompanyModel
                {
                    Id = buyOffer.Company.Id,
                    Name = buyOffer.Company.Name
                }

            };
        }

        public CompanyModel ConvertCompany(Company company)
        {
            return new CompanyModel
            {
                Id = company.Id,
                Name = company.Name
            };
        }

        public ResourceModel ConvertResource(Resource resource)
        {
            return new ResourceModel
            {
                Id = resource.Id,
                UserId = resource.UserId,
                Amount = resource.Amount,
                Company = new CompanyModel { Id = resource.Company.Id, Name = resource.Company.Name }
            };
        }

        public SellOfferModel ConvertSellOffer(SellOffer sellOffer)
        {
            return new SellOfferModel
            {
                Id = sellOffer.Id,
                StartAmount = sellOffer.StartAmount,
                Date = sellOffer.Date,
                Amount = sellOffer.Amount,
                Price = sellOffer.Price,
                IsValid = sellOffer.IsValid,
                Company = new CompanyModel
                {
                    Id = sellOffer.Company.Id,
                    Name = sellOffer.Company.Name
                }
            };
        }

        public TransactionModel ConvertTransaction(Transaction transaction)
        {
            return new TransactionModel
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                BuyOfferId = transaction.BuyOfferId, 
                Date = transaction.Date,
                Price = transaction.Price,
                SellOfferId = transaction.SellOfferId,
                Company = new CompanyModel
                {
                    Id = transaction.Company.Id,
                    Name = transaction.Company.Name
                },
                Type = transaction.Type == TransactionType.BUY_OFFER ? TransactionTypeModel.BUY_OFFER : TransactionTypeModel.SELL_OFFER
            };
        }

        public UserModel ConvertUser(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Cash = user.Cash
            };
        }
    }
}
