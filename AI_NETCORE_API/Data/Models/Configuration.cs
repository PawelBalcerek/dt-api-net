using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("Configurations")]
    public partial class Configuration
    {
        [Key]
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
