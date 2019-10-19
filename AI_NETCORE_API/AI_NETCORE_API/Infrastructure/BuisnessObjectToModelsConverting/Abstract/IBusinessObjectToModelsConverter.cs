using AI_NETCORE_API.Models.Objects;
using Data.BuisnessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract
{
    public interface IBusinessObjectToModelsConverter
    {
        CompanyModel ConvertCompany(Company company);
        BuyOfferModel ConvertBuyOffer(BuyOffer buyOffer);
        SellOfferModel ConvertSellOffer(SellOffer sellOffer);
        UserModel ConvertUser(User user);
        TransactionModel ConvertTransaction(Transaction transaction);
        ResourceModel ConvertResource(Resource resource);

    }
}
