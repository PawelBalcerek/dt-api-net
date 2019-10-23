using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("Configurations")]
    public partial class Configuration
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
