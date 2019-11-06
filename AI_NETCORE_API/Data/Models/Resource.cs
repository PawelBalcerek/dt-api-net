using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("Resources")]
    public partial class Resource
    {
        public Resource()
        {
            BuyOffers = new HashSet<BuyOffer>();
            SellOffers = new HashSet<SellOffer>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompId { get; set; }
        public int Amount { get; set; }

        public virtual Company Comp { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BuyOffer> BuyOffers { get; set; }
        public virtual ICollection<SellOffer> SellOffers { get; set; }
    }
}
