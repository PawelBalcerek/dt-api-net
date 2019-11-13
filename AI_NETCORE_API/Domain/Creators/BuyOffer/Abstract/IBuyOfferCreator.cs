using Domain.Creators.BuyOffer.Request.Abstract;
using Domain.Creators.BuyOffer.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.BuyOffer.Abstract
{
    public interface IBuyOfferCreator
    {
        IBuyOfferCreateResponse CreateBuyOffer(IBuyOfferCreateRequest buyOfferCreateRequest);
    }
}
