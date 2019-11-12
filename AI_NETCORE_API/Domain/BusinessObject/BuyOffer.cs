using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class BuyOffer : BaseOffer
    {
        public BuyOffer(int id, int resourceId, int amount, DateTime date, bool isValid, double maxPrice) : base(id,resourceId, amount, date, isValid)
        {
            MaxPrice = maxPrice;
        }

        public double MaxPrice { get; }
    }
}
