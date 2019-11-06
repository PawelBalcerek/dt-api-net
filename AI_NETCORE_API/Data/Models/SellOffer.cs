using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("SellOffers")]
    public partial class SellOffer
    {
        public SellOffer()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public bool IsValid { get; set; }
        public int StartAmount { get; set; }

        public virtual Resource Resource { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
