using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.SellOffers.Request.Abstract
{
    public interface IGetSellOfferByIdRequest
    {
        int SellOfferId { get; }
    }
}
