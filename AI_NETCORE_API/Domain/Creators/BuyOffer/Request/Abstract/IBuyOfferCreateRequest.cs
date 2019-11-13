using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.BuyOffer.Request.Abstract
{
   public interface IBuyOfferCreateRequest
    {
        int CompanyId { get; }
        int Amount { get; }
        double Price { get; }
    }
}
