using Domain.Creators.SellOffer.Request.Abstract;
using Domain.Creators.SellOffer.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.SellOffer.Abstract
{
    public interface ISellOfferCreator
    {
        ISellOfferCreateResponse CreateSellOffer(ISellOfferCreateRequest sellOfferCreateRequest);
    }
}
