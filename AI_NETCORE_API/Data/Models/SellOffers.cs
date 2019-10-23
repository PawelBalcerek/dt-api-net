using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class SellOffers
    {
        public SellOffers()
        {
            Transactions = new HashSet<Transactions>();
        }

        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public byte[] Date { get; set; }
        public bool IsValid { get; set; }
        public int StartAmount { get; set; }

        public Resources Resource { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
    }
}
