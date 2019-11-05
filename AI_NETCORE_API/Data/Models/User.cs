using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("Users")]
    public partial class User
    {
        public User()
        {
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Cash { get; set; }

        public ICollection<Resource> Resources { get; set; }
    }
}
