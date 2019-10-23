using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Transactions
    {
        public int Id { get; set; }
        public int SellOfferId { get; set; }
        public int BuyOfferId { get; set; }
        public byte[] Date { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }

        public BuyOffers BuyOffer { get; set; }
        public SellOffers SellOffer { get; set; }
    }
}
