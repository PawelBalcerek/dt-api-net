using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.SellOffers.Request.Abstract
{
    public interface IGetSellOffersByUserIdRequest
    {
        int UserId { get; }
    }
}
