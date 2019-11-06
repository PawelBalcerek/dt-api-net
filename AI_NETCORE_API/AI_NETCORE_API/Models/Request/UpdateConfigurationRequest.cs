using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Request
{

    public class UpdateConfigurationAPIRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Value { get; set; }
    }
}
