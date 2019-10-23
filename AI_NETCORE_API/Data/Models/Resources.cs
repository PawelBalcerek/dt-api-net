using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Resources
    {
        public Resources()
        {
            BuyOffers = new HashSet<BuyOffers>();
            SellOffers = new HashSet<SellOffers>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompId { get; set; }
        public int Amount { get; set; }

        public Companies Comp { get; set; }
        public Users User { get; set; }
        public ICollection<BuyOffers> BuyOffers { get; set; }
        public ICollection<SellOffers> SellOffers { get; set; }
    }
}
