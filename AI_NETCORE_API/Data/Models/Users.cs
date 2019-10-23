using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Users
    {
        public Users()
        {
            Resources = new HashSet<Resources>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Cash { get; set; }

        public ICollection<Resources> Resources { get; set; }
    }
}
