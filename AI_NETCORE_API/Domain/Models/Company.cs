using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("Companies")]
    public partial class Company
    {
        public Company()
        {
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Resource> Resources { get; set; }
    }
}
