using System;
using System.Collections.Generic;
using System.Text;

namespace Data.BuisnessObject
{
    public class BuyOffer : BaseOffer
    {
        public BuyOffer(int id, int resourceId, int amount, DateTime date, bool isValid, decimal maxPrice) : base(id, resourceId, amount, date, isValid)
        {
            MaxPrice = maxPrice;
        }

        public decimal MaxPrice { get; }
    }
}
