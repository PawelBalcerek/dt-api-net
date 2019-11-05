using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("BuyOffers")]
    public partial class BuyOffer
    {
        public BuyOffer()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int Amount { get; set; }
        public decimal MaxPrice { get; set; }
        public DateTime Date { get; set; }
        public bool IsValid { get; set; }
        public int StartAmount { get; set; }

        public virtual Resource Resource { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
