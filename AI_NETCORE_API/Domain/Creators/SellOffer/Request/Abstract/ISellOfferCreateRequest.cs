using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.SellOffer.Request.Abstract
{
    public interface ISellOfferCreateRequest
    {
        int ResourceId { get; }
        int Amount { get; }
        decimal Price { get; }
    }
}
