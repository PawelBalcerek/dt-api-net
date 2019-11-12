using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOToBOConverting
{
    public class DTOToBOConverter : IDTOToBOConverter
    {
        public Domain.BusinessObject.User ConvertUser(Data.Models.User user)
        {
            if (user == null)
                return null;
            else
                return new Domain.BusinessObject.User(user.Id, user.Name, user.Email, user.Password, user.Cash);
        }
        public Domain.BusinessObject.Company ConvertCompany(Data.Models.Company company)
        {
            if (company == null)
                return null;
            else
                return new Domain.BusinessObject.Company(company.Id, company.Name);
        }
        public Domain.BusinessObject.BuyOffer ConvertBuyOffer(Data.Models.BuyOffer buyOffer)
        {
            if (buyOffer == null)
                return null;
            else
                return new Domain.BusinessObject.BuyOffer(buyOffer.Id, buyOffer.ResourceId, buyOffer.Amount, buyOffer.Date, buyOffer.IsValid, buyOffer.MaxPrice);
        }
        public Domain.BusinessObject.SellOffer ConvertSellOffer(Data.Models.SellOffer sellOffer)
        {
            if (sellOffer == null)
                return null;
            else
                return new Domain.BusinessObject.SellOffer(sellOffer.Id, sellOffer.ResourceId, sellOffer.Amount, sellOffer.Date, sellOffer.IsValid, sellOffer.Price, sellOffer.StartAmount, new BusinessObject.Company(sellOffer.Resource.Comp.Id, sellOffer.Resource.Comp.Name));
        }
        public Domain.BusinessObject.Resource ConvertResource(Data.Models.Resource resource)
        {
            if (resource == null)
                return null;
            else
                return new Domain.BusinessObject.Resource(resource.Id, resource.UserId, new BusinessObject.Company(resource.CompId, resource.Comp.Name), resource.Amount);
        }
        public Domain.BusinessObject.Transaction ConvertTransaction(Data.Models.Transaction transaction)
        {
            if (transaction == null)
                return null;
            else
                return new Domain.BusinessObject.Transaction(transaction.Id, transaction.SellOfferId, transaction.BuyOfferId, transaction.Date, transaction.Price, transaction.Amount);
        }
        public Domain.BusinessObject.Configuration ConvertConfiguration(Data.Models.Configuration configuration)
        {
            if (configuration == null)
                return null;
            else
                return new Domain.BusinessObject.Configuration(configuration.Name, configuration.Value);
        }
    }
}
