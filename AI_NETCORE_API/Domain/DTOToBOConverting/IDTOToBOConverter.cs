using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOToBOConverting
{
    public interface IDTOToBOConverter
    {
        Domain.BusinessObject.User ConvertUser(Data.Models.User user);
        Domain.BusinessObject.Company ConvertCompany(Data.Models.Company company);
        Domain.BusinessObject.BuyOffer ConvertBuyOffer(Data.Models.BuyOffer buyOffer);
        Domain.BusinessObject.SellOffer ConvertSellOffer(Data.Models.SellOffer sellOffer);
        Domain.BusinessObject.Resource ConvertResource(Data.Models.Resource resource);
        Domain.BusinessObject.Transaction ConvertTransaction(Data.Models.Transaction transaction, Data.Models.Company company);
        Domain.BusinessObject.Configuration ConvertConfiguration(Data.Models.Configuration configuration);
    }
}
