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
                ResourceId = sellOffer.ResourceId,
                StartAmount = sellOffer.StartAmount,
                Date = sellOffer.Date,
                Amount = sellOffer.Amount,
                Price = sellOffer.Price,
                Company = new CompanyModel
                {
                    Id = sellOffer.Company.Id,
                    Name = sellOffer.Company.Name
                }
            };
        }

        List<SellOfferModel> ConvertSellOffer(List<SellOffer> sellOffer)
        {
            List<SellOfferModel> list = new List<SellOfferModel>();

            foreach(var element in sellOffer)
            {
                list.Add(ConvertSellOffer(element));
            }
            return list;
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
