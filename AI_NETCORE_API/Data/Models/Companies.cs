using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Companies
    {
        public Companies()
        {
            Resources = new HashSet<Resources>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Resources> Resources { get; set; }
    }
}
