using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using Domain.BuisnessObject;
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
                Date = buyOffer.Date,
                IsValid = buyOffer.IsValid,
                MaxPrice = buyOffer.MaxPrice,
                ResourceId = buyOffer.ResourceId
                
            };
        }

        public CompanyModel ConvertCompany(Company company)
        {
            return new CompanyModel
            {
                Id = company.Id,
                Name = company.Name,
                UserId = company.UserId
            };
        }

        public ResourceModel ConvertResource(Resource resource)
        {
            return new ResourceModel
            {
                Id = resource.Id,
                UserId = resource.UserId,
                Amount = resource.Amount,
                CompanyId = resource.CompanyId
            };
        }

        public SellOfferModel ConvertSellOffer(SellOffer sellOffer)
        {
            return new SellOfferModel
            {
                Id = sellOffer.Id,
                ResourceId = sellOffer.ResourceId,
                IsValid = sellOffer.IsValid,
                Date = sellOffer.Date,
                Amount = sellOffer.Amount,
                Price = sellOffer.Price
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
                SellOfferId = transaction.SellOfferId
            };
        }

        public UserModel ConvertUser(User user)
        {
            return new UserModel
            {
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}
